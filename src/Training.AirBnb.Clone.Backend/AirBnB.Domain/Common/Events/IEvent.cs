using MediatR;

namespace AirBnB.Domain.Common.Events;

/// <summary>
/// Represents an interface for events in a MediatR-based system.
/// </summary>
public interface IEvent : INotification
{
    
}