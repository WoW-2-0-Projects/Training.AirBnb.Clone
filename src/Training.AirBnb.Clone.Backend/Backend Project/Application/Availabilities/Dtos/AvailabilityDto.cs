namespace Backend_Project.Application.Availabilities.Dtos;

public class AvailabilityDto
{
    public Guid Id { get; set; }

    public int MinNights { get; set; }

    public int MaxNights { get; set; }

    public int? PreparationDays { get; set; }

    public int AvailabilityWindow { get; set; } = 3;
}