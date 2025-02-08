using MassTransit;

namespace Hotel.Api.Consumers;

internal sealed class BookHotelConsumerDefinition 
    : ConsumerDefinition<BookHotelConsumer>
{
    private readonly static string _consumerName = "book-hotel-saga-queue";

    public BookHotelConsumerDefinition()
    {
        EndpointName = _consumerName;
    }

    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<BookHotelConsumer> consumerConfigurator,
        IRegistrationContext context)
    {
        endpointConfigurator.DiscardSkippedMessages();
        endpointConfigurator.ConfigureConsumeTopology = true;

        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}