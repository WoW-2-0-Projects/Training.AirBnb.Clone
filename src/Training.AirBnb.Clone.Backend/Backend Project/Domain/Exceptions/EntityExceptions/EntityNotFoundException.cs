namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string? message) : base(message)
    {
    }
}

public class EntityNotFoundException<T> : EntityNotFoundException
{
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string message) : base(message)
    {
    }
}