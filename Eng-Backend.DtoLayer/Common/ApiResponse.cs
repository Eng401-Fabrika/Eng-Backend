namespace Eng_Backend.DtoLayer.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(bool success, int statusCode, string message, T? data = default, List<string>? errors = null)
    {
        Success = success;
        StatusCode = statusCode;
        Message = message;
        Data = data;
        Errors = errors;
    }

    // Success response
    public static ApiResponse<T> SuccessResponse(T? data, string message = "Success", int statusCode = 200)
    {
        return new ApiResponse<T>(true, statusCode, message, data);
    }

    // Error response
    public static ApiResponse<T> ErrorResponse(string message, int statusCode = 400, List<string>? errors = null)
    {
        return new ApiResponse<T>(false, statusCode, message, default, errors);
    }
}

// Non-generic version for responses without data
public class ApiResponse : ApiResponse<object>
{
    public ApiResponse() : base()
    {
    }

    public ApiResponse(bool success, int statusCode, string message, object? data = null, List<string>? errors = null)
        : base(success, statusCode, message, data, errors)
    {
    }

    public static ApiResponse SuccessResponse(string message = "Success", int statusCode = 200)
    {
        return new ApiResponse(true, statusCode, message);
    }

    public static new ApiResponse ErrorResponse(string message, int statusCode = 400, List<string>? errors = null)
    {
        return new ApiResponse(false, statusCode, message, null, errors);
    }
}
