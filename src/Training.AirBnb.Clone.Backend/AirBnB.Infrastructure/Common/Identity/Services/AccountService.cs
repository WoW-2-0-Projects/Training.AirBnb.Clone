﻿using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Verifications.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

public class AccountService(
    IUserService userService,
    IUserRepository userRepository,
    IUserInfoVerificationCodeService userInfoVerificationCodeService
    ) : IAccountService
{
    public async ValueTask<User?> GetUserByEmailAddressAsync(string emailAddress, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return await userRepository.Get(asNoTracking: asNoTracking)
            .FirstOrDefaultAsync(user => user.EmailAddress == emailAddress,
                cancellationToken: cancellationToken);
    }

    public async ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var createdUser = await userService.CreateAsync(user, cancellationToken: cancellationToken);

        // send welcome email
        
        // send verification email
        
        return true;
    }

    public async ValueTask<bool> VerifyUserAsync(string code, CancellationToken cancellationToken = default)
    {
        var userVerifyCode = await userInfoVerificationCodeService.GetByCodeAsync(code, cancellationToken);

        if (!userVerifyCode.IsValid) return false;

        var user = await userService.GetByIdAsync(userVerifyCode.Code.UserId, cancellationToken: cancellationToken) ??
                   throw new InvalidOperationException();

        switch (userVerifyCode.Code.CodeType)
        {
            case VerificationCodeType.EmailAddressVerification:
                user.IsEmailAddressVerified = true;
                await userService.UpdateAsync(user, false, cancellationToken);
                break;
            default: throw new NotSupportedException();
        }

        await userInfoVerificationCodeService.DeactivateAsync(userVerifyCode.Code.Id,
            cancellationToken: cancellationToken);

        return true;
    }
}