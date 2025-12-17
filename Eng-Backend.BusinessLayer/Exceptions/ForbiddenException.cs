namespace Eng_Backend.BusinessLayer.Exceptions;

public class ForbiddenException : HttpException
{
    public ForbiddenException(string message = "Access forbidden", List<string>? errors = null) 
        : base(403, message, errors)
    {
    }
}
