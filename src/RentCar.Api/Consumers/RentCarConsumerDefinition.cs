using MassTransit;

namespace RentCar.Api.Consumers;

internal sealed class RentCarConsumerDefinition
    : ConsumerDefinition<RentCarConsumer>
{
    private readonly static string _consumerName = "rent-car-saga-queue";

    public RentCarConsumerDefinition()
    {
        EndpointName = _consumerName;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<RentCarConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardSkippedMessages();
        endpointConfigurator.ConfigureConsumeTopology = true;

        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}