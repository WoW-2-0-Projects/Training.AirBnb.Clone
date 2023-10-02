namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class EntityNotFoundException<T> : EntityException<T>
{
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string message) : base(message)
    {
    }
}