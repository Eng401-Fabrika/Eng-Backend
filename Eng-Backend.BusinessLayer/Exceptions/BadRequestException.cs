namespace Eng_Backend.BusinessLayer.Exceptions;

public class BadRequestException : HttpException
{
    public BadRequestException(string message, List<string>? errors = null) 
        : base(400, message, errors)
    {
    }
}
