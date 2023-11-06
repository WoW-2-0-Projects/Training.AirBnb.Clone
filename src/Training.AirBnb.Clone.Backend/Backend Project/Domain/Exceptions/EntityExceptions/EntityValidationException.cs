namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class EntityValidationException : Exception
{
    public EntityValidationException()
    {
    }

    public EntityValidationException(string message) : base(message)
    {
    }
}

public class EntityValidationException<T> : EntityValidationException
{
    public EntityValidationException()
    {
    }

    public EntityValidationException(string message) : base(message)
    {
    }
}