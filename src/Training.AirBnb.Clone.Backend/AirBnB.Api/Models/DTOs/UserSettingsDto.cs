using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Api.Models.DTOs;

public class UserSettingsDto
{
    /// <summary>
    /// gets or sets the unique identifier for the entity
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets user preferred theme of user
    /// </summary>
    public Theme PreferredTheme { get; set; }

    /// <summary>
    /// Gets or sets user preferred notification type of user
    /// </summary>
    public NotificationType PreferredNotificationType { get; set; }

    /// <summary>
    /// User's id for relation
    /// </summary>
    public Guid UserId { get; set; }

}
