using AirBnB.Domain.Common.Events;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.Events;

/// <summary>
/// This class represents an event that is triggered when a user is created.
/// </summary>
public class UserCreatedEvent(User createdUser) : Event
{
    /// <summary>
    /// Gets or sets the information about the user that was created.
    /// </summary>
    public User CreatedUser { get; set; } = createdUser;
}