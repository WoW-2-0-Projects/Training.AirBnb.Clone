using AirBnB.Application.Common.EventBus.Brokers;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Notifications.Events;
using AirBnB.Application.Common.Verifications.Services;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

public class AccountService(
    IEvenBusBroker evenBusBroker,
    IUserService userService,
    IUserRepository userRepository,
    IUserSettingsService userSettingsService,
    IUserInfoVerificationCodeService userInfoVerificationCodeService
) : IAccountService
{
    public async ValueTask<User?> GetUserByEmailAddressAsync(
        string emailAddress,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return await userRepository.Get(asNoTracking: asNoTracking)
            .FirstOrDefaultAsync(user => user.EmailAddress == emailAddress, cancellationToken: cancellationToken);
    }

    public async ValueTask<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        // Create user
        var createdUser = await userService.CreateAsync(user, cancellationToken: cancellationToken);
        
        // Create user settings
        await userSettingsService.CreateAsync(
            new UserSettings
            {
                UserId = createdUser.Id
            },
            cancellationToken: cancellationToken
        );
        
        // TODO : Send welcome email
        var welcomeNotificationEvent = new ProcessNotificationEvent
        {
            ReceiverUserId = createdUser.Id,
            TemplateType = NotificationTemplateType.SystemWelcomeNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.UserNamePlaceholder, createdUser.FirstName }
            }
        };

        // send verification email
        await evenBusBroker.PublishAsync(
            welcomeNotificationEvent,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName,
            cancellationToken
        );

        return createdUser;
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

        await userInfoVerificationCodeService.DeactivateAsync(userVerifyCode.Code.Id, cancellationToken: cancellationToken);

        return true;
    }
}