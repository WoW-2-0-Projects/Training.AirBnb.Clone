namespace Backend_Project.Persistance.DataContexts;

public interface IDataContext
{
    ValueTask SaveChangesAsync();
}
