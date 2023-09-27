using Backend_Project.Domain.Common;
namespace Backend_Project.Domain.Entities
{
    public class Address : SoftDeletedEntity
    {
        public Guid CountryId { get; set; }
        public Guid CityId { get; set; }
        public string Province { get; set; }
        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? AddressLine4 { get; set; }
        public string? ZipCode { get; set; }
        public Address(Guid countryId, Guid cityId, string province, string addressLine1,
            string ? addressLine2 = null, string? addressLine3 = null, string? addressLine4 = null, string? zipCode = null)
        {
            Id = Guid.NewGuid();
            CountryId = countryId;
            CityId = cityId;
            Province = province;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            AddressLine4 = addressLine4;
            ZipCode = zipCode;
            CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}
