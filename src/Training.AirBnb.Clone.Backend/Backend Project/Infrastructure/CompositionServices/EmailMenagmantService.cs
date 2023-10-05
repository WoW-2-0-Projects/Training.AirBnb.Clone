using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Domain.Interfaces;

namespace Backend_Project.Infrastructure.CompositionServices
{
    public class EmailMenagmantService : IEmailMenegmentService
    {
  
        private readonly IEntityBaseService<EmailTemplate> _emailTemplateService;
        private readonly IEmailPlaceholderService _emailPlaceholderService;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IEmailMessageService _emailMessageService;
        private readonly IEntityBaseService<Email> _emailService;
        private readonly IEntityBaseService<User> _userService;

        public EmailMenagmantService(
            IEntityBaseService<EmailTemplate> emailTemplateService,
            IEmailPlaceholderService emailPlaceholderService,
            IEmailSenderService emailSenderService,
            IEmailMessageService emailMessageService,
            IEntityBaseService<Email> emailService,
            IEntityBaseService<User> userService
        )
        {
            _emailTemplateService = emailTemplateService;
            _emailPlaceholderService = emailPlaceholderService;
            _emailSenderService = emailSenderService;
            _emailMessageService = emailMessageService;
            _emailService = emailService;
            _userService = userService;
        }
        public async ValueTask<bool> SendEmailAsync(Guid userId, Guid templateId)
        {
            var template = await _emailTemplateService.GetByIdAsync(templateId) ?? throw new EntityException<EmailTemplate>();

            var placeholders = await _emailPlaceholderService.GetTemplateValues(userId, template);

            var founduser = await _userService.GetByIdAsync(userId) ?? throw new EntityException<User>("User Not found");

            var message = await _emailMessageService.ConvertToMessage(template, placeholders, "sultonbek.rakhimov.recovery@gmail.com", "asadbekrashidov000gmail.com");

            var result = await _emailSenderService.SendEmailAsync(message);

            var email = ToEmail(message);
            email.IsSent = result;

            await _emailService.CreateAsync(email);
            return result;
        }

        private Email ToEmail(EmailMessage message)
        {
            return new Email()
            {
                ReceiverEmailAddress = message.ReceiverAddress,
                SenderEmailAddress = message.SenderAddress,
                Subject = message.Subject,
                Body = message.Body,
            };

        }
    }
}
