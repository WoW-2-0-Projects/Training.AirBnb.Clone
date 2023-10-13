using Backend_Project.Domain.Common;


namespace Backend_Project.Domain.Entities;
public class Description : SoftDeletedEntity
{
    public string ListingDescription { get; set; }
    public string TheSpace { get; set; }
    public string GuestAccess { get; set; }
    public string OtherDetails { get; set; }
    public string InteractionWithGuests { get; set; }
}
