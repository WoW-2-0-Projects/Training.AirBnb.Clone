using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a listing with details such as title, description, status, and pricing.
/// </summary>
public class Listing : SoftDeletedEntity
{
    public string Name { get; set; } = default!;
    
    public DateOnly BuiltDate { get; set; }

    public Address Address { get; set; } = default!;
    
    public Money PricePerNight { get; set; } = default!;
}