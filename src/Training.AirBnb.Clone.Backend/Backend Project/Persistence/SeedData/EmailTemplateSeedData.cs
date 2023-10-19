using Backend_Project.Domain.Entities;
using Backend_Project.Persistence.DataContexts;

namespace Backend_Project.Persistence.SeedData;

public static class EmailTemplateSeedData
{
    public static async ValueTask InitializeEmailTemplateSeedDate(this IDataContext context)
    {
        if(!context.EmailTemplates.Any())
            await InitializeEmailTemplateType(context);
    }
    private static async ValueTask InitializeEmailTemplateType(this IDataContext context)
    {
        var type = new List<EmailTemplate>
        {
            new EmailTemplate {Subject = "Welcome to our website!", Body = "Dear {{FullName}},\n\nWelcome to our website! We hope you enjoy your time here."},
            new EmailTemplate {Subject = "Account activated", Body = "Dear {{FullName}}, We are pleased to inform you that your account has been activated"},
            new EmailTemplate {Subject = "Account update email", Body = "Your account has been updated. Your new account details are as follows"},
            new EmailTemplate {Subject = "Hi {{FullName}}", Body = "Your account {{EmailAddress}} has been restored!Stay with us {{CompanyName}} {{Date}}"},
            new EmailTemplate {Subject = "Your account is back up and running!", Body = "Hi {{FirstName}},\n\nYour account {{EmailAddress}} has been restored and you can now log in. We're glad to have you back up and running!\n\nStay with us {{CompanyName}} {{Date}}"},
            new EmailTemplate {Subject = "Your account is back on track!", Body = "Hi {{FirstName}},\n\nYour account {{EmailAddress}} has been restored and you can now log in. We're here to help you get back on track!\n\nStay with us {{CompanyName}} {{Date}}"},
            new EmailTemplate {Subject = "We'd love to hear from you!", Body = "Dear {FirstName},\n\nWe'd love to hear from you! We're always looking for ways to improve our products and services, and your feedback is invaluable to us."},
            new EmailTemplate {Subject = "We're here to help! ", Body = "Dear {{FullName}},\n\nWe're here to help! If you have any questions or need assistance, please don't hesitate to contact us."}
        };

        await context.EmailTemplates.AddRangeAsync(type);
        await context.SaveChangesAsync();
    } 
}