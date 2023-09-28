namespace Backend_Project.Domain.Exceptions.EmailExceptions;

public class EmailTemplateNotFound : Exception
{
    public EmailTemplateNotFound()
    {

    }

    public EmailTemplateNotFound(string massage) : base(massage) { }

}
