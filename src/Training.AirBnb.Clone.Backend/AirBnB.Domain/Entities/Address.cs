namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a geographical address with information.
/// </summary>
public class Address
{
    /// <summary>
    /// Gets or sets the name of the city in the address.
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the city in the address.
    /// </summary>
    public Guid? CityId { get; set; }

    /// <summary>
    /// Gets or sets the latitude coordinate of the address.
    /// </summary>
    public double Latitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude coordinate of the address.
    /// </summary>
    public double Longitude { get; set; }
}
