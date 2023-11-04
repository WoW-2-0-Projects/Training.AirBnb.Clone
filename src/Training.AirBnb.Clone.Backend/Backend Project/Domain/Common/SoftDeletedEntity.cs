namespace Backend_Project.Domain.Common;

public abstract class SoftDeletedEntity : AuditableEntity, ISoftDeletedEntity
{
    public bool IsDeleted { get; set; } = false;

    public DateTimeOffset? DeletedDate { get; set; }
}