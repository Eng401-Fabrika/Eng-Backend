using Amazon.S3;
using Amazon.S3.Model;
using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Eng_Backend.BusinessLayer.Managers;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _cloudFrontDomain;

    public S3Service(IAmazonS3 s3Client, IConfiguration configuration)
    {
        _s3Client = s3Client;
        _bucketName = configuration["AWS:S3:BucketName"]
            ?? throw new ArgumentNullException("AWS:S3:BucketName configuration is missing");
        _cloudFrontDomain = configuration["AWS:CloudFront:Domain"]
            ?? throw new ArgumentNullException("AWS:CloudFront:Domain configuration is missing");
    }

    public async Task<S3UploadResult> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        try
        {
            // Generate unique file key with folder structure
            var fileKey = $"documents/{DateTime.UtcNow:yyyy/MM/dd}/{Guid.NewGuid()}-{SanitizeFileName(fileName)}";

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileKey,
                InputStream = fileStream,
                ContentType = contentType
            };

            var response = await _s3Client.PutObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return new S3UploadResult
                {
                    Success = true,
                    FileKey = fileKey,
                    FileUrl = GetCloudFrontUrl(fileKey)
                };
            }

            return new S3UploadResult
            {
                Success = false,
                ErrorMessage = "Failed to upload file to S3"
            };
        }
        catch (AmazonS3Exception ex)
        {
            throw new InternalServerException($"S3 upload failed: {ex.Message}");
        }
    }

    public async Task<bool> DeleteFileAsync(string fileKey)
    {
        try
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileKey
            };

            var response = await _s3Client.DeleteObjectAsync(request);
            return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent
                || response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (AmazonS3Exception ex)
        {
            throw new InternalServerException($"S3 delete failed: {ex.Message}");
        }
    }

    public string GetCloudFrontUrl(string fileKey)
    {
        return $"https://{_cloudFrontDomain}/{fileKey}";
    }

    private static string SanitizeFileName(string fileName)
    {
        // Remove invalid characters and spaces
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
        return sanitized.Replace(" ", "_").ToLowerInvariant();
    }
}
