using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

public class Money
{
    public decimal Amount { get; set; }
    
    public Currency Currency { get; set; }
}