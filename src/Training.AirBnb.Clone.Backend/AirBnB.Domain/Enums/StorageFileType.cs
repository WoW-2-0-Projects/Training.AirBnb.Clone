﻿namespace AirBnB.Domain.Enums;

/// <summary>
/// Represents the type of file
/// </summary>
public enum StorageFileType
{
    /// <summary>
    /// Represents an image
    /// </summary>
    Image = 0,

    /// <summary>
    /// Represents Listing Category Image
    /// </summary>
    ListingCategoryImage = 1,
    
    /// <summary>
    /// Represents Listing Image
    /// </summary>
    ListingImage = 2,
    
    /// <summary>
    /// Represents User Profile Image
    /// </summary>
    UserProfileImage = 3
}