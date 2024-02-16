namespace AirBnB.Domain.Constants;

/// <summary>
/// Constants related to the event bus configuration for handling notifications.
/// </summary>
public static class EventBusConstants
{
    /// <summary>
    /// The exchange name for routing notifications within the event bus.
    /// </summary>
    public const string NotificationExchangeName = "Notifications";
    
    /// <summary>
    /// The queue name for processing notifications within the event bus.
    /// </summary>
    public const string ProcessNotificationQueueName = "ProcessNotification";
    
    /// <summary>
    /// The queue name for rendering notifications within the event bus.
    /// </summary>
    public const string RenderNotificationQueueName = "RenderNotification";
    
    /// <summary>
    /// The queue name for sending notifications within the event bus.
    /// </summary>
    public const string SendNotificationQueueName = "SendNotification";
}