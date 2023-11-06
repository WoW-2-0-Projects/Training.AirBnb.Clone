namespace Backend_Project.Application.Notifications.Settings;

public class EmailSenderSettings
{
    public string SmtpClient { get; set; } = default!;

    public int SmtpPort { get; set; }

    public string CredentialEmailAddress { get; set; } = default!;

    public string CredentialPassword { get; set;} = default!;

    public string DateFormat { get; set; } = default!;

    public string CompanyName { get; set; } = default!;
}