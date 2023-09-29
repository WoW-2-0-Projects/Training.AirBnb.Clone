namespace Backend_Project.Domain.Exceptions.NotificationExceptions.EmailTemplateExceptions;

public class EmailTemplateNotFound : Exception
{
    public EmailTemplateNotFound()
    {

    }

    public EmailTemplateNotFound(string massage) : base(massage) { }

}
