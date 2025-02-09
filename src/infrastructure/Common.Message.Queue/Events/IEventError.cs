namespace Common.Message.Queue.Events;

public interface IEventError
{
    Guid TravelerId { get; }
    string Message { get; }
    string StackTrace { get; }
}
