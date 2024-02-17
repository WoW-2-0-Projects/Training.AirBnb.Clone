using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a country entity.
/// </summary>
public class Country : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the country.
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the list of phone number codes associated with the country.
    /// </summary>
    public List<string>? PhoneNumberCodes { get; set; } 
    
    /// <summary>
    /// Gets or sets the currency used in the country.
    /// </summary>
    public Currency? Currency { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the currency used in the country.
    /// </summary>
    public Guid CurrencyId { get; set; }
    
    /// <summary>
    /// Gets or sets the language spoken in the country.
    /// </summary>
    public Language? Language { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the language spoken in the country.
    /// </summary>
    public Guid LanguageId { get; set; }
    
    /// <summary>
    /// Gets or sets the list of cities within the country.
    /// </summary>
    public List<City> Cities { get; set; }
}
