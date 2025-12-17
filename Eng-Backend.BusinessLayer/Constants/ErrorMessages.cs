namespace Eng_Backend.BusinessLayer.Constants;

public static class ErrorMessages
{
    // General
    public const string InvalidId = "Invalid ID provided";
    public const string IdRequired = "{0} ID is required";

    // Auth
    public const string EmailRequired = "Email is required";
    public const string PasswordRequired = "Password is required";
    public const string FullNameRequired = "Full name is required";
    public const string InvalidCredentials = "Invalid email or password";
    public const string UserAlreadyExists = "A user with this email already exists";
    public const string RegistrationFailed = "Registration failed";

    // User
    public const string UserNotFound = "User with ID '{0}' not found";
    public const string UserNotFoundByEmail = "User with email '{0}' not found";

    // Role
    public const string RoleNotFound = "Role with ID '{0}' not found";
    public const string RoleNotFoundByName = "Role '{0}' not found";
    public const string RoleNameRequired = "Role name is required";
    public const string RoleAlreadyExists = "Role '{0}' already exists";
    public const string CannotDeleteSystemRole = "Cannot delete system role '{0}'";
    public const string CannotDeleteRoleWithUsers = "Cannot delete role that is assigned to users. Remove the role from all users first.";
    public const string RoleCreationFailed = "Failed to create role";
    public const string RoleDeletionFailed = "Failed to delete role";
    public const string UserAlreadyHasRole = "User already has role '{0}'";
    public const string UserDoesNotHaveRole = "User does not have role '{0}'";

    // Permission
    public const string PermissionNotFound = "Permission with ID '{0}' not found";
    public const string PermissionAlreadyAssigned = "Permission is already assigned to this role";
    public const string PermissionNotAssigned = "Permission assignment not found for this role";
    public const string InvalidPermissionIds = "Invalid permission IDs: {0}";

    // Document
    public const string DocumentNotFound = "Document with ID '{0}' not found";
    public const string DocumentTitleRequired = "Document title is required";
    public const string FileRequired = "File is required";
    public const string FileUploadFailed = "Failed to upload file";

    // Problem/Task
    public const string ProblemNotFound = "Problem with ID '{0}' not found";
    public const string ProblemTitleRequired = "Problem title is required";
    public const string AssignmentNotFound = "Assignment with ID '{0}' not found";
    public const string AssignmentNotPending = "Assignment is not in pending status";
    public const string AssignmentAlreadyCompleted = "Assignment is already completed";
    public const string CanOnlyAcceptOwnAssignments = "You can only accept your own assignments";
    public const string CanOnlySubmitOwnSolutions = "You can only submit solutions for your own assignments";
    public const string CanOnlyUploadToOwnSolutions = "You can only upload files to your own solutions";
    public const string SolutionNotFound = "Solution with ID '{0}' not found";

    // Quiz
    public const string QuestionNotFound = "Question with ID '{0}' not found";
    public const string QuestionTextRequired = "Question text is required";
    public const string MinimumOptionsRequired = "At least 2 options are required";
    public const string InvalidCorrectAnswerIndex = "Invalid correct answer index";
    public const string AlreadyAnsweredQuestion = "You have already answered this question";

    // Internal
    public const string InternalError = "An internal error occurred";
    public const string InternalErrorWithDetails = "An error occurred: {0}";
}