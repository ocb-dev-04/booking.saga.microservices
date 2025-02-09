namespace Common.Message.Queue.Services;

internal sealed class ExceptionsHandlerService : IExceptionsHandlerService
{

    public async Task ExecuteAsync(Func<Task> action, Action<Exception> errorEventFactory)
    {
        try
        {
            await action.Invoke();
        }
        catch (Exception ex)
        {
            errorEventFactory.Invoke(ex);
        }
    }
}

public interface IExceptionsHandlerService
{
    /// <summary>
    /// Method to hadle invoke or exception
    /// </summary>
    /// <param name="action"></param>
    /// <param name="errorEventFactory"></param>
    /// <returns></returns>
    Task ExecuteAsync(Func<Task> action, Action<Exception> errorEventFactory);
}