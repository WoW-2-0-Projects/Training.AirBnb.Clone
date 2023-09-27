using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class EmailMessage : SoftDeletedEntity
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string SerdorAddress { get; set; }
    public string ReceiverAddress { get; set; }
    public bool IsSent { get; set; }
    public DateTime SentDate { get; set; }

    public EmailMessage(string subject, string body, string serdorAddress,string receiverAddress)
    {
        Subject = subject;
        Body = body;
        SerdorAddress = serdorAddress;
        ReceiverAddress = receiverAddress;
        IsSent = false;
        CreatedDate = DateTime.UtcNow;
    }
}