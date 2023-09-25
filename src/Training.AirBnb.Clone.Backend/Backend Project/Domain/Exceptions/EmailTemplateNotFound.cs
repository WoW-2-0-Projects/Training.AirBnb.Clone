namespace Backend_Project.Domain.Exceptions;

public class EmailTemplateNotFound : Exception
{
    public EmailTemplateNotFound()
    {
        
    }

    public EmailTemplateNotFound(string massage) :base(massage) { }
    
}
