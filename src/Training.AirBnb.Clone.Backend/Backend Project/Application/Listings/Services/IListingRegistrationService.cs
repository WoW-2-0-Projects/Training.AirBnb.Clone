using Backend_Project.Application.Listings.Dtos;
namespace Backend_Project.Application.Listings.Services;

public interface IListingRegistrationService
{
    ValueTask<ListingRegistrationInfoDto> CreateListing(string title);
}