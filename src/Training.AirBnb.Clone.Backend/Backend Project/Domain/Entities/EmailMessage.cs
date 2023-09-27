using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class EmailMessage : SoftDeletedEntity
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public string SendorAddress { get; set; }
    public string ReceiverAddress { get; set; }
    public bool IsSent { get; set; }
    public DateTime SentDate { get; set; }

    public EmailMessage(string subject, string body, string sendorAddress,string receiverAddress)
    {
        Id = Guid.NewGuid();
        Subject = subject;
        Body = body;
        SendorAddress = sendorAddress;
        ReceiverAddress = receiverAddress;
        IsSent = false;
        CreatedDate = DateTime.UtcNow;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Subject, Body, SendorAddress, ReceiverAddress);
    }
    public override bool Equals(object? obj)
    {
        return this.GetHashCode().Equals(obj.GetHashCode());
    }

    public override string ToString()
    {
        return $"Subject:{Subject}\nBody:{Body}\nSendorAddress:{SendorAddress}\nReceiverAddres:{ReceiverAddress}";
    }
}