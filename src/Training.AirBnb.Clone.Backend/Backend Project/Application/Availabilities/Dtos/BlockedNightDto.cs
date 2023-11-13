namespace Backend_Project.Application.Availabilities.Dtos;

public class BlockedNightDto
{
    public Guid Id { get; set; }

    public DateOnly Date { get; set; }

    public Guid ListingId { get; set; }
}