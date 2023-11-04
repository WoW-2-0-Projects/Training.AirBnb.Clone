#pragma warning disable CS8618

using Backend_Project.Application.Locations.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Listings.Dtos;

public class ListingRegistrationInfoDto
{
    public ListingRegistrationDto Listing { get; set; }

    public ListingPropertyTypeRegistrationDto ListingPropertyType { get; set; } = new();

    public AddressDto Address { get; set; } = new();

    public ListingRulesRegistrationDto ListingRules { get; set; } = new();

    public List<ListingProperty> ListingProperties { get; set; } = new();

    public ListingDescriptionRegistrationDto Description { get; set; } = new();

    public RegistrationProgressDto RegistrationProgress { get; set; } = new();
}