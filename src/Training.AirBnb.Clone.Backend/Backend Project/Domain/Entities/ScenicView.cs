using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ScenicView : SoftDeletedEntity
{
    public string Name { get; set; } = default!;
}
