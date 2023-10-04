using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingCategoryFeatureOption : SoftDeletedEntity
{
    public Guid ListingCategoryId { get; set; }
    public Guid ListingFeatureOptionId { get; set; } 
    public Guid ListingFeatureId { get; set; }
    public int FeatureMinValue { get; set; }   
    public int FeatureMaxValue { get; set; }   
}