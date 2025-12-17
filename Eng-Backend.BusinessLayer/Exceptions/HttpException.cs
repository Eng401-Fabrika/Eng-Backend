namespace Eng_Backend.BusinessLayer.Exceptions;

public abstract class HttpException : Exception
{
    public int StatusCode { get; }
    public List<string>? Errors { get; }

    protected HttpException(int statusCode, string message, List<string>? errors = null) 
        : base(message)
    {
        StatusCode = statusCode;
        Errors = errors;
    }
}
