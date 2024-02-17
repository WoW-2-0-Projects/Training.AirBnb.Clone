using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a currency entity.
/// </summary>
public class Currency : Entity
{
    /// <summary>
    /// Gets or sets the name of the currency.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the symbol of the currency.
    /// </summary>
    public string Symbol { get; set; }
    
    /// <summary>
    /// Gets or sets the code of the currency.
    /// </summary>
    public string Code { get; set; }
}
