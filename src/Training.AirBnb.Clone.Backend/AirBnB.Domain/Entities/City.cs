using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a city entity.
/// </summary>
public class City : Entity
{
    /// <summary>
    /// Gets or sets the name of the city.
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the ID of the country to which the city belongs.
    /// </summary>
    public Guid CountryId { get; set; }
    
    /// <summary>
    /// Gets or sets the country to which the city belongs.
    /// </summary>
    public Country Country { get; set; }
}
