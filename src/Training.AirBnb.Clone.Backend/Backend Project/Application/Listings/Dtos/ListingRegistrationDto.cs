#pragma warning disable CS8618

namespace Backend_Project.Application.Listings.Dtos;

public class ListingRegistrationDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public decimal? Price { get; set; }
}