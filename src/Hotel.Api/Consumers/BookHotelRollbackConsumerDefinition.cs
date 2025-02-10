using MassTransit;

namespace Hotel.Api.Consumers;

internal sealed class BookHotelRollbackConsumerDefinition 
    : ConsumerDefinition<BookHotelRollbackConsumer>
{
    private readonly static string _consumerName = "saga-book-hotel-rollback-queue";

    public BookHotelRollbackConsumerDefinition()
    {
        EndpointName = _consumerName;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<BookHotelRollbackConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardSkippedMessages();
        endpointConfigurator.ConfigureConsumeTopology = true;

        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}