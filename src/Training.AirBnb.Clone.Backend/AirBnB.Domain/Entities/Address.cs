namespace AirBnB.Domain.Entities;

public class Address
{
    public string? City { get; set; }
    
    public Guid? CityId { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
}