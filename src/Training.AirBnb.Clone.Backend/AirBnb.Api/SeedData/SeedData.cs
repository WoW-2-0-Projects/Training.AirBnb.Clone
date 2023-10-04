using Backend_Project.Domain.Common;
using Backend_Project.Domain.Entities;
using Backend_Project.Persistance.DataContexts;

namespace Backend_Project.SeedDate;

public static class SeedData
{
    public static async ValueTask InitializeSeedDataAsync(this IDataContext fileContext)
    {
        if (!fileContext.Users.Any())
            await fileContext.AddAsync<User>(100);

        if (!fileContext.EmailTemplates.Any())
            await fileContext.AddAsync<EmailTemplate>(100);
        
        await fileContext.SaveChangesAsync();
    }

    private static async ValueTask AddAsync<TEntity>(this IDataContext context, int count) where TEntity : IEntity
    {
        var task = typeof(TEntity) switch
        {
            { } t when t == typeof(User) => context.AddUsersAsync(count),
            { } t when t == typeof(EmailTemplate) => context.AddEmailTemplate(count),
            _ => new ValueTask(Task.CompletedTask)
        }; 
        
        await task;
    }
    private static async ValueTask AddUsersAsync(this IDataContext context, int count)
    {
        var faker = EntityFakers.GetUserFaker(context);
        var uniqueUsers = new HashSet<User>(faker.Generate(100_000));
        var test = uniqueUsers.Take(count);
        await context.Users.AddRangeAsync(uniqueUsers.Take(count));
    }
    private static async ValueTask AddEmailTemplate(this IDataContext context, int count)
    {
        var list = new List<EmailTemplate> 
        {
            new EmailTemplate() { Subject = "Welcome to our system!", Body = "Hi {{FullName}}, welcome to our system!" },
            new EmailTemplate() { Subject = "Account activated", Body = "Dear {{FullName}}, We are pleased to inform you that your account has been activated. You can now log in and start using our system."},
            new EmailTemplate() { Subject = "Account deleted", Body = "Dear {{FullName}}, We are sorry to inform you that your account has been deleted from our system. This action was taken because [reason for account deletion]." },
            new EmailTemplate() { Subject = "Your password has been reset", Body = "Dear {{FullName}}, Your password has been reset. Your new password is: {{NewPassword}}." },
            new EmailTemplate() { Subject = "Your account has been suspended", Body = "Dear {{FullName}}, Your account has been suspended. Please contact us for more information." },
            new EmailTemplate() { Subject = "Account update email", Body = "Your account has been updated. Your new account details are as follows: [account details]" }
        
        };

        await context.EmailTemplates.AddRangeAsync(list.Take(count));
    }
}
