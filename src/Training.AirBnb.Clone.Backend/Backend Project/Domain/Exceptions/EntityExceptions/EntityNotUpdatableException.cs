namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class EntityNotUpdatableException : Exception
{
    public EntityNotUpdatableException()
    {
    }

    public EntityNotUpdatableException(string message) : base(message)
    {
    }
}

public class EntityNotUpdatableException<T> : EntityNotUpdatableException
{
    public EntityNotUpdatableException()
    {
    }

    public EntityNotUpdatableException(string message) : base(message)
    {
    }
}