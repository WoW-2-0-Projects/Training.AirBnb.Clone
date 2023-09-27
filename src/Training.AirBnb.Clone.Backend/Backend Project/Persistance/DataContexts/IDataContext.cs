using Backend_Project.Domain.Entities;
using FileBaseContext.Abstractions.Models.FileSet;

namespace Backend_Project.Persistance.DataContexts;

public interface IDataContext
{
    IFileSet<User, Guid> Users { get; }
    IFileSet<ListingComment, Guid> ListingComments { get; }
    ValueTask SaveChangesAsync();
}
