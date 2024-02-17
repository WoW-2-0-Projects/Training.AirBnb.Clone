using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a language entity.
/// </summary>
public class Language : Entity
{
    /// <summary>
    /// Gets or sets the name of the language.
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the code of the language.
    /// </summary>
    public string Code { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the locale of the language.
    /// </summary>
    public string Locale { get; set; } = default!;
}
