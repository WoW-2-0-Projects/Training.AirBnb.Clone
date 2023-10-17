using FileBaseContext.Abstractions.Models.Entity;

namespace Backend_Project.Domain.Common;

public interface IEntity : IFileSetEntity<Guid>
{
}