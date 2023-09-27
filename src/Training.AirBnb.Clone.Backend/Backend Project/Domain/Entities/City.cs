using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class City : SoftDeletedEntity
    {
        public string Name { get; set; }
        public Guid CountryId { get; set; }
        public City(string name,Guid countryId)
        {
            Name = name;
            Id = Guid.NewGuid();
            CreatedDate = DateTimeOffset.UtcNow;
            CountryId = countryId;
        }
        public override string ToString()
        {
            return $"{Name}{Id} {CreatedDate}{ModifiedDate}{DeletedDate}{IsDeleted}";
        }
    }
}
