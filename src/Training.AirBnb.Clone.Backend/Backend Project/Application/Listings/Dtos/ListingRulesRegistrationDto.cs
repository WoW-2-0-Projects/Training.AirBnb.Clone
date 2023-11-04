namespace Backend_Project.Application.Listings.Dtos;

public class ListingRulesRegistrationDto
{
    public int Guests { get; set; }

    public int GuestsMinCount { get; set; }

    public int GuestsMaxCount { get; set; }
}