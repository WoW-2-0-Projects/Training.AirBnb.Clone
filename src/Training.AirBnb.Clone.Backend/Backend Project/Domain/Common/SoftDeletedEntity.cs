using System.Text.Json.Serialization;

namespace Backend_Project.Domain.Common;

public abstract class SoftDeletedEntity : AuditableEntity, ISoftDeletedEntity
{
    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
    [JsonIgnore]
    public DateTimeOffset? DeletedDate { get; set; }
}