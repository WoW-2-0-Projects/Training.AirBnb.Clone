using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.Repositories;

public class UserInfoVerificationCodeRepository
    (IdentityDbContext dbContext, ICacheBroker cacheBroker) 
    : EntityRepositoryBase<UserInfoVerificationCode, IdentityDbContext>
        (dbContext, cacheBroker), 
        IUserInfoVerificationCodeRepository
{
    public IQueryable<UserInfoVerificationCode> Get(Expression<Func<UserInfoVerificationCode, bool>>? predicate = default, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<UserInfoVerificationCode?> GetByIdAsync(Guid codeId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(codeId, asNoTracking, cancellationToken);
    }

    public async ValueTask<UserInfoVerificationCode> CreateAsync(UserInfoVerificationCode verificationCode, bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        await DbContext.UserInfoVerificationCodes
            .Where(code => code.Id == verificationCode.Id && code.Type == verificationCode.Type)
            .ExecuteUpdateAsync(setter => setter.SetProperty(code => code.IsActive, false), cancellationToken);

        return await base.CreateAsync(verificationCode, saveChanges, cancellationToken);
    }

    public async ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await DbContext.UserInfoVerificationCodes.Where(code => code.Id == codeId)
            .ExecuteUpdateAsync(setter => setter.SetProperty(code => code.IsActive, false), cancellationToken);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
    }
}