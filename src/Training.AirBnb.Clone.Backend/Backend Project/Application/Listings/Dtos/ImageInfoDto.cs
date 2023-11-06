using Backend_Project.Domain.Enums;

namespace Backend_Project.Application.Listings.Dtos;

public class ImageInfoDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? ListingId { get; set; }

    public string Extension { get; set; } = default!;

    public long Size { get; set; }

    public ImageType Type { get; set; }
}