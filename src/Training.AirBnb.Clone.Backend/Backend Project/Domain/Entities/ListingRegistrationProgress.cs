using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingRegistrationProgress : SoftDeletedEntity
{
    public int Progress { get; set; }

    public Guid ListingId { get; set; }

    public ListingRegistrationProgress(int progress, Guid listingId)
    {
        Progress = progress;
        ListingId = listingId;
    }
}