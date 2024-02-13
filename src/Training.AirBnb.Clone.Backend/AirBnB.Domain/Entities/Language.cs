using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

public class Language : Entity
{
    public string Name { get; set; } = default!;
    
    public string Code { get; set; } = default!;
    
    public string Locale { get; set; } = default!;
}