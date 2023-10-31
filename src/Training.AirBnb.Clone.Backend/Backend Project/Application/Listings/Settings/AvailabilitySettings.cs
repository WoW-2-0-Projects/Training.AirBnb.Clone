namespace Backend_Project.Application.Listings.Settings;

public class AvailabilitySettings
{
    public int MinNights { get; set; }

    public int MaxNights { get; set; }

    public int PreparationMinDays { get; set; }

    public int PreparationMaxDays { get; set; }

    public int AvailabilityWindowMinValue { get; set; }

    public int AvailabilityWindowMaxValue { get; set; }
}