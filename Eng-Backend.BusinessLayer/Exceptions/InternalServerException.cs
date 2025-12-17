namespace Eng_Backend.BusinessLayer.Exceptions;

public class InternalServerException : HttpException
{
    public InternalServerException(string message = "Internal server error", List<string>? errors = null) 
        : base(500, message, errors)
    {
    }
}
