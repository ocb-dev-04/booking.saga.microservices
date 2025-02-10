using MassTransit;

namespace RentCar.Api.Consumers;

internal sealed class RentCarRollbackConsumerDefinition 
    : ConsumerDefinition<RentCarRollbackConsumer>
{
    private readonly static string _consumerName = "saga-rent-car-rollback-queue";

    public RentCarRollbackConsumerDefinition()
    {
        EndpointName = _consumerName;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RentCarRollbackConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardSkippedMessages();
        endpointConfigurator.ConfigureConsumeTopology = true;

        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}