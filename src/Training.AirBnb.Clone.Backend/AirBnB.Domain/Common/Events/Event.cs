using MediatR;

namespace AirBnB.Domain.Common.Events;

public class Event : IEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    
    public bool Redelivered { get; set; }
}