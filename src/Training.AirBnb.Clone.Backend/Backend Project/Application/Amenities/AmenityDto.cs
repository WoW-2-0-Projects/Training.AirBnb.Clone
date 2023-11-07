namespace Backend_Project.Application.Amenities;

public class AmenityDto
{
    public Guid Id { get; set; }

    public string AmenityName { get; set; } = string.Empty;

    public Guid CategoryId { get; set; }
}