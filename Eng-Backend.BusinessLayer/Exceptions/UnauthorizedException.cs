namespace Eng_Backend.BusinessLayer.Exceptions;

public class UnauthorizedException : HttpException
{
    public UnauthorizedException(string message = "Unauthorized access", List<string>? errors = null) 
        : base(401, message, errors)
    {
    }
}
