using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;
public class EmailTemplate : SoftDeletedEntity
{
    public string Subject { get; set; }
    public string Body { get; set; }

    public EmailTemplate()
    {
        
    }
 
    public EmailTemplate(string subject, string body)
    {
        Id = Guid.NewGuid();
        Subject = subject;
        Body = body;
    }


    public override int GetHashCode()
    {
        return HashCode.Combine(Subject, Body);
    }
    public override bool Equals(object? obj)
    {
        return this.GetHashCode().Equals(obj.GetHashCode());
    }
    public override string ToString()
    {
        return $"ID:{Id}, Subject:{Subject} Body:{Body}";
    }
}
