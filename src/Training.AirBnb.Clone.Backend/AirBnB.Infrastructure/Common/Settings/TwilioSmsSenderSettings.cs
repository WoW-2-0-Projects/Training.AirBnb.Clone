namespace AirBnB.Infrastructure.Common.Settings;
/// <summary>
/// Represents the Twilio sms sender settings required for sending sms messages.
/// </summary>
public class TwilioSmsSenderSettings
{
    /// <summary>
    /// Gets or sets the id of the Accounts.
    /// </summary>
    public string AccountsId { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the token of Twilio Auth.
    /// </summary>
    public string AuthToken { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the phone number used as the sender for Twilio SMS messages.
    /// </summary>
    public string SenderPhoneNumber { get; set; } = default!;
}