using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class Country : SoftDeletedEntity
    {
        public string Name { get; set; }
        public string CountryDialingCode { get; set; }
        public int RegionPhoneNumberLength { get; set; }

        public Country(string name, string countryDialingCode, int regionPhoneNumberLength)
        {
            Name = name;
            Id = Guid.NewGuid();
            CreatedDate = DateTimeOffset.UtcNow;
            CountryDialingCode = countryDialingCode;
            RegionPhoneNumberLength = regionPhoneNumberLength;
        }
    }
}