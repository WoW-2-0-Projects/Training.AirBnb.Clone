namespace AirBnB.Domain.Commons;

public interface IAuditableEntity : IEntity
{
    public DateTime CreateAt { get; set; }

    public DateTime Update { get; set; }
}