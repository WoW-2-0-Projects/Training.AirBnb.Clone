namespace Backend_Project.Domain.Exceptions.EntityExceptions;

public class EntityValidationException<T> : EntityException<T>
{
    public EntityValidationException(string message) : base(message)
    {
    }
}