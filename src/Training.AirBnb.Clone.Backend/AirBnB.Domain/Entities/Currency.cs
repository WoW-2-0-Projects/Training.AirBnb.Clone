using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

public class Currency : Entity
{
    public string Name { get; set; }
    
    public string Symbol { get; set; }
    
    public string Code { get; set; }
}