namespace AirBnB.Domain.Brokers;

public interface IRequestUserContextProvider
{
    Guid GetUserId();
}