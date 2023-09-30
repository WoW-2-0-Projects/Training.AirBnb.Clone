using Backend_Project.Domain.Entities;
using FileBaseContext.Abstractions.Models.FileSet;
namespace Backend_Project.Persistance.DataContexts;

public interface IDataContext : IAsyncDisposable
{
    IFileSet<EmailTemplate,Guid> EmailTemplates { get; }
    IFileSet<EmailMessage,Guid> EmailMessages { get; }
    IFileSet<Reservation, Guid> Reservations { get; }
    IFileSet<User, Guid> Users { get; }
    IFileSet<ListingComment, Guid> ListingComments { get; }
    IFileSet<UserCredentials,Guid> UserCredentials { get; }

    ValueTask SaveChangesAsync();
}