using MassTransit;

namespace Flight.Api.Consumers;

internal sealed class BookFlightConsumerDefinition
    : ConsumerDefinition<BookFlightConsumer>
{
    private readonly static string _consumerName = "booking-flight-saga-queue";

    public BookFlightConsumerDefinition()
    {
        EndpointName = _consumerName;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<BookFlightConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardSkippedMessages();
        endpointConfigurator.ConfigureConsumeTopology = true;

        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}