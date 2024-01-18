using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class EmailTemplateRepository(AppDbContext dbContext, ICacheBroker cacheBroker):
    EntityRepositoryBase<EmailTemplate, AppDbContext>(dbContext, cacheBroker, new CacheEntryOptions()), IEmailTemplateRepository
{
    public new IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? predicate = default,
        bool asNoTracking = false) => base.Get(predicate, asNoTracking);

    public new ValueTask<IList<EmailTemplate>> GetAsync(
        QuerySpecification<EmailTemplate> querySpecification,
        CancellationToken cancellationToken = default) =>
        base.GetAsync(querySpecification, cancellationToken);

    public new ValueTask<IList<EmailTemplate>> GetAsync(
        QuerySpecification querySpecification,
        CancellationToken cancellationToken = default) =>
        base.GetAsync(querySpecification, cancellationToken);
    

    public new async ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        await base.CreateAsync(emailTemplate, saveChanges, cancellationToken);
}