namespace AirBnB.Domain.Entities;

public class Rating
{
    /// <summary>
    /// Gets or sets the cleanliness rating provided by the guest (1 to 5).
    /// </summary>
    public float Cleanliness { get; set; }

    /// <summary>
    /// Gets or sets the accuracy rating provided by the guest (1 to 5).
    /// </summary>
    public float Accuracy { get; set; }

    /// <summary>
    /// Gets or sets the check-in rating provided by the guest (1 to 5).
    /// </summary>
    public float CheckIn { get; set; }

    /// <summary>
    /// Gets or sets the communication rating provided by the guest (1 to 5).
    /// </summary>
    public float Communication { get; set; }

    /// <summary>
    /// Gets or sets the location rating provided by the guest (1 to 5).
    /// </summary>
    public float Location { get; set; }

    /// <summary>
    /// Gets or sets the value rating provided by the guest (1 to 5).
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// Gets or sets the calculated overall rating based on the individual aspect ratings.
    /// </summary>
    public float OverallRating { get; set; }
}