#pragma warning disable CS8618

namespace Backend_Project.Application.Location.Dtos;

public class AddressDto
{
    public Guid? CityId { get; set; }
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? AddressLine3 { get; set; }
    public string? AddressLine4 { get; set; }
    public string? Province { get; set; }
    public string? ZipCode { get; set; }
}