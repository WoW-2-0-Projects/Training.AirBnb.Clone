using MediatR;

namespace AirBnB.Domain.Common.Events;

/// <summary>
/// Represents an interface for events in a MediatR-based system.
/// </summary>
public interface IEvent : INotification
{
    public Guid Id { get; set; }
    
    public DateTimeOffset CreatedTime { get; set; }
    
    public bool Redelivered { get; set; }
}