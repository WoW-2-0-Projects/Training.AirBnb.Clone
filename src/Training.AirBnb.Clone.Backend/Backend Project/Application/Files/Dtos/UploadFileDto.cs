using Backend_Project.Domain.Enums;

namespace Backend_Project.Application.Files.Dtos;

public class UploadFileDto
{
    public string ContentType { get; set; } = default!;

    public long Size { get; set; }

    public ImageType Type { get; set; }

    public Stream Source { get; set; } = default!;

    public Guid UserId { get; set; }

    public Guid? ListingId { get; set; }
}