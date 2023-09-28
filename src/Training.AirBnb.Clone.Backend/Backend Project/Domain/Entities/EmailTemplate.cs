using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;
public class EmailTemplate : SoftDeletedEntity
{
    public string Subject { get; set; }
    public string Body { get; set; }
 
    public EmailTemplate(string subject, string body)
    {
        Id = Guid.NewGuid();
        Subject = subject;
        Body = body;
        CreatedDate = DateTimeOffset.UtcNow;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Subject, Body);
    }

    public override bool Equals(object? obj)
    {
        if (obj != null && obj is EmailTemplate) 
            return GetHashCode().Equals(obj.GetHashCode());

        return false;
    }

    public override string ToString()
    {
        return $"ID:{Id}, Subject:{Subject} Body:{Body}";
    }
}