using Common.Message.Queue.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Message.Queue;

public static class CommnoMessageQueueServices
{
    public static void AddCommonMessageQueueServices(this IServiceCollection services)
    {
        services.AddTransient<IExceptionsHandlerService, ExceptionsHandlerService>();
    }
}
