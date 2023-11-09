#pragma warning disable CS8618

namespace Backend_Project.Application.ListingCategoryDetails.Dtos;

public class ListingTypeDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}