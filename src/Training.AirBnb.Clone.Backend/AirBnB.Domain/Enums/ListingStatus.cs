namespace AirBnB.Domain.Enums;

/// <summary>
/// Represents the status of a listing.
/// </summary>
public enum ListingStatus
{
    /// <summary>
    /// The listing is actively listed and publicly visible.
    /// </summary>
    Listed = 0,
    
    /// <summary>
    /// The listing is not publicly visible.
    /// </summary>
    Unlisted = 1,
    
    /// <summary>
    /// The listing is currently in progress or undergoing changes.
    /// </summary>
    InProgress = 2
}