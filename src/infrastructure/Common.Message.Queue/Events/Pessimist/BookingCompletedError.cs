namespace Common.Message.Queue.Events;

public sealed record BookingCompletedError(
    Guid CorrelationId, 
    string Message,
    string StackTrace);
