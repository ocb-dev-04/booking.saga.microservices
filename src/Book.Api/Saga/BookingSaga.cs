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
    public State HotelBookingError { get; set; }
    public Event<HotelBooked> HotelBooked { get; set; }
    public Event<HotelBookedError> HotelBookedError { get; set; }

    // step 2
    public State FlightBooking { get; set; }
    public State FlightBookingError { get; set; }
    public Event<FlightBooked> FlightBooked { get; set; }
    public Event<FlightBookedError> FlightBookedError { get; set; }

    // step 3
    public State CarRenting { get; set; }
    public State CarRentingError { get; set; }
    public Event<CarRented> CarRented { get; set; }
    public Event<CarRentedError> CarRentedError { get; set; }

    // final step
    public State BookingCompleting { get; set; }
    public Event<BookingCompleted> BookingCompleted { get; set; }

    public BookingSaga()
    {
        InstanceState(x => x.CurrentState);

        //optimist
        Event(() => BookingInitialized, e => e.CorrelateById(m => m.Message.TravelerId));
        Event(() => HotelBooked, e => e.CorrelateById(m => m.Message.TravelerId));
        Event(() => FlightBooked, e => e.CorrelateById(m => m.Message.TravelerId));
        Event(() => CarRented, e => e.CorrelateById(m => m.Message.TravelerId));
        Event(() => BookingCompleted, e => e.CorrelateById(m => m.Message.TravelerId));

        // pessimist
        Event(() => HotelBookedError, e => e.CorrelateById(m => m.Message.TravelerId));
        Event(() => FlightBookedError, e => e.CorrelateById(m => m.Message.TravelerId));
        Event(() => CarRentedError, e => e.CorrelateById(m => m.Message.TravelerId));

        Initially(
            When(BookingInitialized)
                .Then(context
                    =>
                {
                    context.Saga.TravelerId = context.Message.TravelerId;
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
                        context.Saga.TravelerId,
                        context.Saga.Email,
                        context.Saga.HotelName)));

        During(
            BookingInitialize,
             When(HotelBooked)
                .Then(context
                    =>
                {
                    context.Saga.HotelRegistrationId = context.Message.TravelerId;
                    context.Saga.HotelBooked = true;
                })
                .TransitionTo(FlightBooking)
                .Publish(context
                    => new BookFlightRequest(
                        context.Saga.TravelerId,
                        context.Saga.FlightFrom,
                        context.Saga.FlightTo,
                        context.Saga.FlightCode)));

        During(
            FlightBooking,
            When(FlightBooked)
                .Then(context
                    => context.Saga.FlightBooked = true)
                .TransitionTo(CarRenting)
                .Publish(context
                    => new RentCarRequest(
                        context.Saga.TravelerId,
                        context.Saga.CarPlateNumber)));

        During(
            CarRenting,
            When(CarRented)
                .Then(context
                    => context.Saga.CarRented = true)
                .TransitionTo(BookingCompleting)
                .Publish(context
                    => new BookingCompleted(context.Message.TravelerId)));

        During(
            BookingCompleting,
            When(BookingCompleted)
                .Then(context
                    => context.Saga.BookingFinished = true)
                .Finalize());
    }
}