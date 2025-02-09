using MassTransit;
using Common.Message.Queue.Events;
using Common.Message.Queue.Commands;

namespace Book.Api.Saga;

internal sealed class BookingSaga : MassTransitStateMachine<BookingSagaData>
{
    public State BookingInitialize { get; set; }
    public Event<BookingInitialized> BookingInitialized { get; set; }

    // step 1
    public State HotelBooking { get; set; }
    public Event<HotelBooked> HotelBooked { get; set; }
    public Event<HotelBookedError> HotelBookedError { get; set; }

    // step 2
    public State FlightBooking { get; set; }
    public Event<FlightBooked> FlightBooked { get; set; }
    public Event<FlightBookedError> FlightBookedError { get; set; }

    // step 3
    public State CarRenting { get; set; }
    public Event<CarRented> CarRented { get; set; }
    public Event<CarRentedError> CarRentedError { get; set; }

    // final step
    public State BookingCompleting { get; set; }
    public Event<BookingCompleted> BookingCompleted { get; set; }
    public Event<BookingCompletedError> BookingCompletedError { get; set; }

    public BookingSaga()
    {
        InstanceState(x => x.CurrentState);

        //optimist
        Event(() => BookingInitialized, e => e.CorrelateById(m => m.Message.CorrelationId));
        Event(() => HotelBooked, e => e.CorrelateById(m => m.Message.CorrelationId));
        Event(() => FlightBooked, e => e.CorrelateById(m => m.Message.CorrelationId));
        Event(() => CarRented, e => e.CorrelateById(m => m.Message.CorrelationId));
        Event(() => BookingCompleted, e => e.CorrelateById(m => m.Message.CorrelationId));

        // pessimist
        Event(() => HotelBookedError, e => e.CorrelateById(m => m.Message.CorrelationId));
        Event(() => FlightBookedError, e => e.CorrelateById(m => m.Message.CorrelationId));
        Event(() => CarRentedError, e => e.CorrelateById(m => m.Message.CorrelationId));
        Event(() => BookingCompletedError, e => e.CorrelateById(m => m.Message.CorrelationId));

        #region Optimist events

        Initially(
            When(BookingInitialized)
                .Then(context
                    =>
                {
                    context.Saga.Email = context.Message.Email;

                    context.Saga.HotelName = context.Message.HotelName;

                    context.Saga.FlightFrom = context.Message.FlightCode;
                    context.Saga.FlightTo = context.Message.FlightCode;
                    context.Saga.FlightCode = context.Message.FlightCode;

                    context.Saga.CarPlateNumber = context.Message.CarPlateNumber;
                })
                .TransitionTo(BookingInitialize)
                .Publish(context
                    => new BookHotelRequest(
                        context.Saga.CorrelationId,
                        context.Saga.Email,
                        context.Saga.HotelName)));

        During(
            BookingInitialize,
             When(HotelBooked)
                .Then(context
                    =>
                {
                    context.Saga.TravelerId = context.Message.TravelerId;
                    context.Saga.HotelRegistrationId = context.Message.TravelerId;
                    context.Saga.HotelBooked = true;
                })
                .TransitionTo(FlightBooking)
                .Publish(context
                    => new BookFlightRequest(
                        context.Saga.CorrelationId,
                        context.Saga.TravelerId,
                        context.Saga.FlightFrom,
                        context.Saga.FlightTo,
                        context.Saga.FlightCode)));

        During(
            FlightBooking,
            When(FlightBooked)
                .Then(context
                    =>
                {
                    context.Saga.FlightRegistrationId = context.Message.RegistrationId;
                    context.Saga.FlightBooked = true;
                })
                .TransitionTo(CarRenting)
                .Publish(context
                    => new RentCarRequest(
                        context.Message.CorrelationId,
                        context.Saga.TravelerId,
                        context.Saga.CarPlateNumber)));

        During(
            CarRenting,
            When(CarRented)
                .Then(context
                    =>
                {
                    context.Saga.CarRegistrationId = context.Message.RegistrationId;
                    context.Saga.CarRented = true;
                })
                .TransitionTo(BookingCompleting)
                .Publish(context
                    => new BookingCompleted(
                            context.Message.CorrelationId,
                            context.Message.TravelerId)));

        During(
            BookingCompleting,
            When(BookingCompleted)
                .Then(context
                    =>
                {
                    context.Saga.SuccessOnUtc = DateTimeOffset.UtcNow;
                    context.Saga.BookingFinished = true;
                })
                .Finalize());

        #endregion

        #region Pessimist events

        During(
            BookingCompleting,
            When(BookingCompletedError)
                .Then(context
                    =>
                {
                    if (context.Saga.SomeErrorOcurred) return;

                    context.Saga.ErrorMessage = context.Message.Message;
                    context.Saga.StackTrace = context.Message.StackTrace;
                    context.Saga.SomeErrorOcurred = true;
                })
                .TransitionTo(CarRenting)
                .Publish(context
                    => new RentCarRollbackRequest(
                            context.Message.CorrelationId,
                            context.Saga.CarRegistrationId)));

        During(
            CarRenting,
            When(CarRentedError)
                .Then(context
                    =>
                {
                    if (context.Saga.SomeErrorOcurred) return;

                    context.Saga.ErrorMessage = context.Message.Message;
                    context.Saga.StackTrace = context.Message.StackTrace;
                    context.Saga.SomeErrorOcurred = true;
                })
                .TransitionTo(FlightBooking)
                .Publish(context
                    => new BookFlightRollbackRequest(
                            context.Message.CorrelationId,
                            context.Saga.FlightRegistrationId)));

        During(
            FlightBooking,
            When(FlightBookedError)
                .Then(context
                    =>
                {
                    if (context.Saga.SomeErrorOcurred) return;

                    context.Saga.ErrorMessage = context.Message.Message;
                    context.Saga.StackTrace = context.Message.StackTrace;
                    context.Saga.SomeErrorOcurred = true;
                })

                .TransitionTo(HotelBooking)
                .Publish(context
                    => new BookHotelRollbackRequest(
                            context.Message.CorrelationId,
                            context.Saga.HotelRegistrationId)));

        During(
            HotelBooking,
            When(HotelBookedError)
                .Then(context
                    =>
                {
                    if (context.Saga.SomeErrorOcurred) return;

                    context.Saga.ErrorMessage = context.Message.Message;
                    context.Saga.StackTrace = context.Message.StackTrace;
                    context.Saga.SomeErrorOcurred = true;
                })
                .Finalize());

        #endregion
    }
}