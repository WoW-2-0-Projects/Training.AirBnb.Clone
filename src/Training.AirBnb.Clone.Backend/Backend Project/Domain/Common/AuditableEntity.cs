using System.Text.Json.Serialization;

namespace Backend_Project.Domain.Common;

public abstract class AuditableEntity : Entity, IAuditableEntity
{
    [JsonIgnore]
    public DateTimeOffset CreatedDate { get; set; }
    [JsonIgnore]
    public DateTimeOffset? ModifiedDate { get; set; }
}