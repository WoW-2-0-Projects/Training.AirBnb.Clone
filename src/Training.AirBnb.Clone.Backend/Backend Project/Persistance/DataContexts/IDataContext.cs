using Backend_Project.Domain.Entities;
using FileBaseContext.Abstractions.Models.FileSet;
namespace Backend_Project.Persistance.DataContexts;

public interface IDataContext
{
    IFileSet<EmailTemplate,Guid> EmailTemplates { get; }
    IFileSet<Reservation, Guid> Reservations { get; }
    IFileSet<User, Guid> Users { get; }
    IFileSet<ListingRating, Guid> ListingRatings { get; }
    ValueTask SaveChangesAsync();
}
