namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class EntityNotDeletableException<T> : EntityException<T>
{
    public EntityNotDeletableException()
    {
    }

    public EntityNotDeletableException(string message) : base(message)
    {
    }
}