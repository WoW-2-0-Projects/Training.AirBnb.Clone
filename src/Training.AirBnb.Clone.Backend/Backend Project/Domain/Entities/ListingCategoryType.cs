using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingCategoryType : SoftDeletedEntity
{
    public Guid ListingCategoryId { get; set; }
    public Guid ListingTypeId { get; set; }  
}