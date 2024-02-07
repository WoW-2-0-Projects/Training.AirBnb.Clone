namespace AirBnB.Domain.Constants;

public class EventBusConstants
{
    #region Notifications

    public const string NotificationExchangeName = "Notifications";
    public const string ProcessNotificationQueueName = "ProcessNotification";
    public const string RenderNotificationQueueName = "RenderNotification";
    public const string SendNotificationQueueName = "SendNotification";

    #endregion
}