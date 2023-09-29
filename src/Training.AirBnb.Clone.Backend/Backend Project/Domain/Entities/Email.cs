using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Email : SoftDeletedEntity
{
    public Guid SendUserId { get; set; }
    public Guid ReceiverUserId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set;}
    public string ReceiverEmailAddres { get; set; }
    public string SenderEmailAddress { get; set; }
    public DateTimeOffset SendTime { get; set; }
    public bool IsSent { get; set; }

    public Email(string subject, string body,string receiverEmailAddress,string senderEmailAddress)
    {
        Id = Guid.NewGuid();
        SendUserId = Guid.NewGuid();
        ReceiverUserId = Guid.NewGuid();
        Subject = subject;
        Body = body;
        ReceiverEmailAddres = receiverEmailAddress;
        SenderEmailAddress = senderEmailAddress;
        SendTime = DateTimeOffset.UtcNow;
        IsSent = false;

    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Subject,Body, ReceiverEmailAddres, SenderEmailAddress);
    }
    public override bool Equals(object? obj)
    {
        if(obj != null && obj is null)
        {
            return GetHashCode().Equals(obj.GetHashCode());
        }
        return false;
    }
    public override string ToString()
    {
        return $"Subject:{Subject}\nBody:{Body}\nReceiverEmailAddres:{ReceiverEmailAddres}\nSendorEmailAddress:{SendorEmailAddress}";
    }
}
