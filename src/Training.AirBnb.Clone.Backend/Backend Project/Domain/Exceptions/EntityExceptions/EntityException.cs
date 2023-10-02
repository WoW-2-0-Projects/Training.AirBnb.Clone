namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class EntityException<T> : Exception
{
    public EntityException()
    {
    }

    public EntityException(string message) : base(message)
    {
    }
}