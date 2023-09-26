using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class City : SoftDeletedEntity
    {
        public string Name { get; set; }
        public City(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
            CreatedDate = DateTimeOffset.UtcNow;

        }
        public override string ToString()
        {
            return $"{Name}{Id} {CreatedDate}{ModifiedDate}{DeletedDate}{IsDeleted}";
        }
    }
}
