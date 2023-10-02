using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Interfaces;
public interface IEmailPlaceholderService
{
    ValueTask<Dictionary<string, string>> GEtTemplateValues(Guid userId,EmailTemplate emailTemplate);
}
