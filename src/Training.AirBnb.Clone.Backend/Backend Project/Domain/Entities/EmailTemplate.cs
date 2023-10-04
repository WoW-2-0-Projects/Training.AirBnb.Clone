using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;
public class EmailTemplate : SoftDeletedEntity
{
    public string Subject { get; set; }
    public string Body { get; set; }

    public override int GetHashCode()
        => HashCode.Combine(Subject, Body);
    
    public override bool Equals(object? obj)
    {
        if (obj is EmailTemplate) 
            return GetHashCode().Equals(obj.GetHashCode());

        return false;
    }
}