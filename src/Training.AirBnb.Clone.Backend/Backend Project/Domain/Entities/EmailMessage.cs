using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class EmailMessage : AuditableEntity
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string SenderAddress { get; set; }
    public string ReceiverAddress { get; set; }
    public bool IsSent { get; set; }
    public DateTimeOffset SendDate { get; set; }

    public EmailMessage(string subject, string body, string senderAddress,string receiverAddress)
    {
        Id = Guid.NewGuid();
        Subject = subject;
        Body = body;
        SenderAddress = senderAddress;
        ReceiverAddress = receiverAddress;
        IsSent = false;
        CreatedDate = DateTimeOffset.UtcNow;

    }
    public override string ToString()
    {
        return $"Subject:{Subject}\nBody:{Body}\nSendorAddress:{SenderAddress}\nReceiverAddress:{ReceiverAddress}\nCreatedDate:{CreatedDate}";
    }
}