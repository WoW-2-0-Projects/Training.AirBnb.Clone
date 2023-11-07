namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class EntityNotDeletableException : Exception
{
    public EntityNotDeletableException()
    {
    }

    public EntityNotDeletableException(string message) : base(message)
    {
    }
}

public class EntityNotDeletableException<T> : EntityNotDeletableException
{
    public EntityNotDeletableException()
    {
    }

    public EntityNotDeletableException(string message) : base(message)
    {
    }
}