using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

public class Role : IEntity
{
    public Guid Id { get; set; }

    public RoleType Type { get; set; }

    public bool IsDisable { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime ModifiedTime { get; set; }
}
