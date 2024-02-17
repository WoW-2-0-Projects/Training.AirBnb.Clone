using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a monetary value.
/// </summary>
public class Money
{
    /// <summary>
    /// Gets or sets the currency id.
    /// </summary>
    public Guid CurrencyId { get; set; }
    
    /// <summary>
    /// Gets or sets the numerical amount of money.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the currency associated with the monetary amount.
    /// </summary>
    public Currency Currency { get; set; }
}