namespace Backend_Project.Application.Notifications.Dtos;

public class EmailDto
{
    public Guid Id { get; set; }
    
    public Guid ReceiverUserId { get; set; }
    
    public string? Subject { get; set; }
    
    public string? Body { get; set;}
    
    public string? ReceiverEmailAddress { get; set; }
    
    public string? SenderEmailAddress { get; set; }
    
    public bool IsSent { get; set; } 
}