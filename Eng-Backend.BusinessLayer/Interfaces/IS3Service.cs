namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IS3Service
{
    Task<S3UploadResult> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    Task<bool> DeleteFileAsync(string fileKey);
    string GetCloudFrontUrl(string fileKey);
}

public class S3UploadResult
{
    public bool Success { get; set; }
    public string FileKey { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
}
