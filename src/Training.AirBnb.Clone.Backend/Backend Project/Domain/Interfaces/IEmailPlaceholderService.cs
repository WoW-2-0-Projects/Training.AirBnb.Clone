using Backend_Project.Domain.Entities;

namespace Backend_Project.Domain.Interfaces;

public interface IEmailPlaceholderService
{
    ValueTask<Dictionary<string, string>> GEtTemplateValues(Guid userId,EmailTemplate emailTemplate);
}
