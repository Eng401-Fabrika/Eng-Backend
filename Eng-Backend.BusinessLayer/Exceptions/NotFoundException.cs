namespace Eng_Backend.BusinessLayer.Exceptions;

public class NotFoundException : HttpException
{
    public NotFoundException(string message = "Resource not found", List<string>? errors = null) 
        : base(404, message, errors)
    {
    }
}
