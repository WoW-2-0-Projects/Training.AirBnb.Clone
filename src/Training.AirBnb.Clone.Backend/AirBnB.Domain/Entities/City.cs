using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

public class City : Entity
{
    public string Name { get; set; } = default!;
    
    public Guid CountryId { get; set; }
    
    public Country Country { get; set; }
}