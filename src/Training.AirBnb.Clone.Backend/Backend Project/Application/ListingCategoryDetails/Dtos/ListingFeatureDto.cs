#pragma warning disable CS8618

namespace Backend_Project.Application.ListingCategoryDetails.Dtos;

public class ListingFeatureDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public int MinValue { get; set; }

    public int MaxValue { get; set; }

    public Guid ListingTypeId { get; set; }
}