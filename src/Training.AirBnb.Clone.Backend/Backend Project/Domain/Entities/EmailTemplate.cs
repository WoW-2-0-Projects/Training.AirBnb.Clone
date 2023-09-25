using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;
public class EmailTemplate : SoftDeletedEntity
{
    public string Subject { get; set; }
    public string Body { get; set; }

    public EmailTemplate()
    {
        
    }

    public EmailTemplate(Guid id, string subject, string body)
    {
        Id = id;
        Subject = subject;
        Body = body;
    }

}
