#pragma warning disable CS8618

using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class Address : SoftDeletedEntity
{
    public Guid CountryId { get; set; }
    public Guid CityId { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string? Province { get; set; }
    public string? ZipCode { get; set; }
}