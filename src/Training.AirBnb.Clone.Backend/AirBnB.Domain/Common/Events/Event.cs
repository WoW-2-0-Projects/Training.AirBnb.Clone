namespace AirBnB.Domain.Common.Events;

public class Event
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
    
    public bool Redelivered { get; set; }
}