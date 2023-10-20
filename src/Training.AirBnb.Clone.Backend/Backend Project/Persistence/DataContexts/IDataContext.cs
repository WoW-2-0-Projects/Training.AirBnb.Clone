using Backend_Project.Domain.Entities;
using FileBaseContext.Abstractions.Models.FileSet;

namespace Backend_Project.Persistence.DataContexts;

public interface IDataContext
{
    IFileSet<City, Guid> Cities { get; }
    IFileSet<Country, Guid> Countries { get; }
    IFileSet<EmailTemplate, Guid> EmailTemplates { get; }
    IFileSet<Email, Guid> Emails { get; }
    IFileSet<EmailMessage, Guid> EmailMessages { get; }
    IFileSet<Reservation, Guid> Reservations { get; }
    IFileSet<User, Guid> Users { get; }
    IFileSet<Address, Guid> Addresses { get; }
    IFileSet<ReservationOccupancy, Guid> ReservationOccupancies { get; }
    IFileSet<Comment, Guid> Comments { get; }
    IFileSet<UserCredentials, Guid> UserCredentials { get; }
    IFileSet<ListingCategory, Guid> ListingCategories { get; }
    IFileSet<AmenityCategory, Guid> AmenityCategories { get; }
    IFileSet<ListingRating, Guid> ListingRatings { get; }
    IFileSet<Amenity, Guid> Amenities { get; }
    IFileSet<Rating, Guid> Ratings { get; }
    IFileSet<ListingType, Guid> ListingTypes { get; }
    IFileSet<ListingCategoryType, Guid> ListingCategoryTypes { get; }
    IFileSet<ListingFeature, Guid> ListingFeatures { get; }
    IFileSet<ListingProperty, Guid> ListingProperties { get; }
    IFileSet<ListingAmenities, Guid> ListingAmenities { get; }
    IFileSet<ListingOccupancy, Guid> ListingOccupancies { get; }
    IFileSet<Listing, Guid> Listings { get; }
    IFileSet<ListingRules, Guid> ListingRules { get; }
    IFileSet<Location, Guid> Locations { get; }
    IFileSet<PhoneNumber, Guid> PhoneNumbers { get; }
    IFileSet<ListingPropertyType, Guid> ListingPropertyTypes { get; }
    IFileSet<Description, Guid> Descriptions { get; }
    IFileSet<Availability, Guid> Availabilities { get; }

    ValueTask SaveChangesAsync();
}