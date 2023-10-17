using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingOccupancy : SoftDeletedEntity
{
    public int Guests { get; set; }
    public bool AllowPets { get; set; }
}