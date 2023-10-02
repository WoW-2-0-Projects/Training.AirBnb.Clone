using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingCategoryFeatureOption : SoftDeletedEntity
{
    public Guid ListingCategoryId { get; set; }
    public Guid ListingFeatureOptionId { get; set; } 
    public Guid ListingFeatureId { get; set; }
    public int FeatureMinValue { get; set; }   
    public int FeatureMaxValue { get; set; }   

    public ListingCategoryFeatureOption(Guid listingCategoryId, Guid listingFeatureOptionId, Guid listingFeatureId, int featureMinValue, int featureMaxValue)
    {
        Id = Guid.NewGuid();
        ListingCategoryId = listingCategoryId;
        ListingFeatureOptionId = listingFeatureOptionId;
        ListingFeatureId = listingFeatureId;
        FeatureMinValue = featureMinValue;
        FeatureMaxValue = featureMaxValue;
        CreatedDate = DateTimeOffset.UtcNow;
    }
}