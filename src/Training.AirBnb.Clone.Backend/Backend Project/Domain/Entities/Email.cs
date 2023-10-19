#pragma warning disable CS8618

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
}