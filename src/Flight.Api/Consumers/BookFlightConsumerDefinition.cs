using MassTransit;

namespace Flight.Api.Consumers;

internal sealed class BookFlightConsumerDefinition
    : ConsumerDefinition<BookFlightConsumer>
{
    private readonly static string _consumerName = "saga-booking-flight-queue";

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