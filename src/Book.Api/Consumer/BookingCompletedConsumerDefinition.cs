using MassTransit;

namespace Book.Api.Consumer;

internal sealed class BookingCompletedConsumerDefinition
    : ConsumerDefinition<BookingCompletedConsumer>
{
    private readonly static string _consumerName = "booking-completed-saga-queue";

    public BookingCompletedConsumerDefinition()
    {
        EndpointName = _consumerName;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<BookingCompletedConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardSkippedMessages();
        endpointConfigurator.ConfigureConsumeTopology = true;

        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}