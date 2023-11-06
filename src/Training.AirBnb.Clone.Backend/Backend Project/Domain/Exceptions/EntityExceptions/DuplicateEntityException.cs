namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class DuplicateEntityException : Exception
{
    public DuplicateEntityException()
    {
    }

    public DuplicateEntityException(string message) : base(message)
    {
    }
}

public class DuplicateEntityException<T> : DuplicateEntityException
{
    public DuplicateEntityException()
    {
    }

    public DuplicateEntityException(string message) : base(message)
    {
    }
}