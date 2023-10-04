#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Country : SoftDeletedEntity
{
    public string Name { get; set; }
    public string CountryDialingCode { get; set; }
    public int RegionPhoneNumberLength { get; set; }
}