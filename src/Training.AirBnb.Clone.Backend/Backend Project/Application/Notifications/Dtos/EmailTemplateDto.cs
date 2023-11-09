namespace Backend_Project.Application.Notifications.Dtos;

public class EmailTemplateDto
{
    public Guid Id { get; set; }
    
    public string? Subject { get; set; }
    
    public string? Body { get; set; }
}