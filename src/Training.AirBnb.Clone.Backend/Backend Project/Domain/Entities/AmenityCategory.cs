using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class AmenityCategory : SoftDeletedEntity
    {
        public string CategoryName { get; set; }

        public AmenityCategory(string categoryName)
        {
            Id = Guid.NewGuid();
            CategoryName = categoryName;
            CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}
