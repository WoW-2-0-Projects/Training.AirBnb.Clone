#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class PhoneNumber : SoftDeletedEntity
{
    public string UserPhoneNumber { get; set; }
    public string Code { get; set; }
    public Guid CountryId { get; set; }

    public PhoneNumber(string userPhoneNumber, string code, Guid countryId)
    {
        Id = Guid.NewGuid();
        UserPhoneNumber = userPhoneNumber;
        Code = code;
        CountryId = countryId;
        CreatedDate = DateTimeOffset.UtcNow;
    }

}