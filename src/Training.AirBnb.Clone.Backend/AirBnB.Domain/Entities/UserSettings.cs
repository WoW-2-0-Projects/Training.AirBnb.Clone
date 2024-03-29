﻿using AirBnB.Domain.Common.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a user settings entity
/// </summary>
public class UserSettings : AuditableEntity
{
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

    /// <summary>
    /// User property for includes
    /// </summary>
    public virtual User? User { get; set; }
}
