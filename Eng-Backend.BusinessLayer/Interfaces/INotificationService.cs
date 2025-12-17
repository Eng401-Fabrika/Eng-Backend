namespace Eng_Backend.BusinessLayer.Interfaces;

public interface INotificationService
{
    Task SendProblemAssignedNotificationAsync(Guid userId, string userEmail, string problemTitle, DateTime? dueDate);
    Task SendSolutionReviewedNotificationAsync(Guid userId, string userEmail, string problemTitle, bool approved, int pointsAwarded);
}