using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

public class Country : AuditableEntity
{
    public string Name { get; set; } = default!;
    
    public List<string>? PhoneNumber { get; set; } 
    
    public Currency? Currency { get; set; }
    
    public Guid CurrencyId { get; set; }
    
    public Language? Language { get; set; }
    
    public Guid LanguageId { get; set; }
    
    public List<City> Cities { get; set; }
}