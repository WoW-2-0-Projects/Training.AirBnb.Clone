using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Availability : SoftDeletedEntity
{
    public int MinNights { get; set; }  
    public int MaxNights { get; set;}
    public int? PreparationDays { get; set; }
    public int AvailabilityWindow { get; set; } = 3;
}