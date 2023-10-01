using Backend_Project.Domain.Entities;
using FileBaseContext.Abstractions.Models.FileSet;

namespace Backend_Project.Persistance.DataContexts;

public interface IDataContext : IAsyncDisposable
{
    IFileSet<City, Guid> Cities { get; }
    IFileSet<Country, Guid> Countries { get; }
    IFileSet<EmailTemplate,Guid> EmailTemplates { get; }
    IFileSet<Email,Guid> Emails { get; }
    IFileSet<Reservation, Guid> Reservations { get; }
    IFileSet<User, Guid> Users { get; }
    IFileSet<Address, Guid> Addresses { get; }   
    IFileSet<ReservationOccupancy, Guid> ReservationOccupancies { get; }
    IFileSet<Comment, Guid> Comments { get; }
    IFileSet<UserCredentials,Guid> UserCredentials { get; }
    IFileSet<ListingCategory, Guid> ListingCategories { get; }
    IFileSet<AmenityCategory, Guid> AmenityCategories { get; }
    IFileSet<ListingRating, Guid> ListingRatings { get; }
    IFileSet<Amenity, Guid> Amenities { get; }

    ValueTask SaveChangesAsync();
}