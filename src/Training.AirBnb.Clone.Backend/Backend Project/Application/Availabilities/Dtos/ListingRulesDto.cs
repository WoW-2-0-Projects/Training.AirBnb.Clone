namespace Backend_Project.Application.Availabilities.Dtos;

public class ListingRulesDto
{
    public Guid Id { get; set; }

    public int Guests { get; set; }

    public bool PetsAllowed { get; set; }

    public bool EventsAllowed { get; set; }

    public bool SmokingAllowed { get; set; }

    public bool CommercialFilmingAllowed { get; set; }

    public TimeOnly? CheckInTimeStart { get; set; }

    public TimeOnly? CheckInTimeEnd { get; set; }

    public TimeOnly CheckOutTime { get; set; }

    public string? AdditionalRules { get; set; } = string.Empty;
}