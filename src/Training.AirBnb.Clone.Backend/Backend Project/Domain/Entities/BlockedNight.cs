using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class BlockedNight : SoftDeletedEntity
    {
        public DateOnly Date {  get; set; }
        public bool IsCustomBlock { get; set; }
        public Guid ListingId { get; set; }
    }
}
