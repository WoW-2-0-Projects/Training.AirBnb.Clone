using Backend_Project.Domain.Entities;
using FileBaseContext.Abstractions.Models.FileSet;

namespace Backend_Project.Persistance.DataContexts;

public interface IDataContext : IAsyncDisposable
{
    IFileSet<EmailTemplate, Guid> EmailTemplates { get; }
    ValueTask SaveChangesAsync();
}
