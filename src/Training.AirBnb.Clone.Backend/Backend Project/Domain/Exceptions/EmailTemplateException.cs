namespace Backend_Project.Domain.Exceptions;

public class EmailTemplateException : Exception
{
    public EmailTemplateException()
    {
        
    }

    public EmailTemplateException(string massage) :base(massage) { }
    
}
