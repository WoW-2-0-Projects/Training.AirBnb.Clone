namespace Backend_Project.Domain.Interfaces;

public interface IEmailMessageService<T>
{
    ValueTask<T> ConvertToMessage(T entity, Dictionary<string, string> values, string sender, string receiver);
}
