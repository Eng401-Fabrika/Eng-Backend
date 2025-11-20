namespace Eng_Backend.BusinessLayer.Utils;

public class ServiceResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public object Data { get; set; } // Token vb. dönmek için

    public static ServiceResult Ok(object data, string message = "İşlem Başarılı") 
        => new ServiceResult { Success = true, Data = data, Message = message };

    public static ServiceResult Fail(string message) 
        => new ServiceResult { Success = false, Message = message };
}