using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class ListingOccupancy : SoftDeletedEntity
    {
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Infants {  get; set; }
        public int Pets { get; set; }
        public ListingOccupancy(int adults, int children,int infats, int pets)
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTimeOffset.UtcNow;
            Adults = adults;
            Children = children;
            Infants = infats;
            Pets = pets;
        }
    }
}
