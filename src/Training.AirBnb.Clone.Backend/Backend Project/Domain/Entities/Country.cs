using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class Country : SoftDeletedEntity
    {
        public string Name { get; set; }
        public Country(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}