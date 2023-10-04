using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Email : SoftDeletedEntity
{
    public Guid SendUserId { get; set; }
    public Guid ReceiverUserId { get; set; }
    public string Subject { get; set; }
    public string Body { get; set;}
    public string ReceiverEmailAddress { get; set; }
    public string SenderEmailAddress { get; set; }
    public bool IsSent { get; set; }

/*    public Email(Guid sendUserId,Guid receiverUserId,string subject, string body,string receiverEmailAddress,string senderEmailAddress)
    {
        Id = Guid.NewGuid();
        SendUserId = sendUserId;
        ReceiverUserId = receiverUserId;
        Subject = subject;
        Body = body;
        ReceiverEmailAddress = receiverEmailAddress;
        SenderEmailAddress = senderEmailAddress;
        CreatedDate = DateTimeOffset.UtcNow;
        IsSent = false;

    }*/

}
