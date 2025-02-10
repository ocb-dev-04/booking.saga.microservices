using MassTransit;

namespace Flight.Api.Consumers;

internal sealed class BookFlightRollbackConsumerDefinition
    : ConsumerDefinition<BookFlightRollbackConsumer>
{
    private readonly static string _consumerName = "saga-booking-flight-rollback-queue";

    public BookFlightRollbackConsumerDefinition()
    {
        EndpointName = _consumerName;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<BookFlightRollbackConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardSkippedMessages();
        endpointConfigurator.ConfigureConsumeTopology = true;

        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}