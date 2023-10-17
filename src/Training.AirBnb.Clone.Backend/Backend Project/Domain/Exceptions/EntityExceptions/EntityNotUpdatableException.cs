namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class EntityNotUpdatableException<T> : EntityException<T>
{
    public EntityNotUpdatableException()
    {
    }

    public EntityNotUpdatableException(string message) : base(message)
    {
    }
}