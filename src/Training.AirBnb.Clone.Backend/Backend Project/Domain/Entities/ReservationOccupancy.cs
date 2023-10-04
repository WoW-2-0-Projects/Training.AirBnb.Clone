using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ReservationOccupancy : SoftDeletedEntity
{
    public int Adults { get; set; }
    public int Children { get; set; }
    public int Infants {  get; set; }
    public int Pets { get; set; }
}