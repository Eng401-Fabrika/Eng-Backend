using Eng_Backend.BusinessLayer.Interfaces;
using Microsoft.Extensions.Logging;

namespace Eng_Backend.BusinessLayer.Managers;

/// <summary>
/// Mock notification service - logs notifications instead of sending emails
/// TODO: Replace with real email service (SendGrid, AWS SES, etc.)
/// </summary>
public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public Task SendProblemAssignedNotificationAsync(Guid userId, string userEmail, string problemTitle, DateTime? dueDate)
    {
        var dueDateStr = dueDate?.ToString("dd/MM/yyyy HH:mm") ?? "Belirtilmemiş";

        _logger.LogInformation(
            "[EMAIL NOTIFICATION] To: {Email} | Subject: Yeni Görev Atandı | " +
            "Body: Sayın kullanıcı, '{ProblemTitle}' başlıklı bir görev size atandı. " +
            "Son tarih: {DueDate}",
            userEmail, problemTitle, dueDateStr);

        // TODO: Implement real email sending
        // await _emailSender.SendEmailAsync(userEmail, "Yeni Görev Atandı", $"...");

        return Task.CompletedTask;
    }

    public Task SendSolutionReviewedNotificationAsync(Guid userId, string userEmail, string problemTitle, bool approved, int pointsAwarded)
    {
        var status = approved ? "ONAYLANDI" : "REDDEDİLDİ";
        var pointsStr = approved ? $"+{pointsAwarded} puan kazandınız!" : "";

        _logger.LogInformation(
            "[EMAIL NOTIFICATION] To: {Email} | Subject: Çözümünüz Değerlendirildi | " +
            "Body: '{ProblemTitle}' için gönderdiğiniz çözüm {Status}. {Points}",
            userEmail, problemTitle, status, pointsStr);

        return Task.CompletedTask;
    }
}