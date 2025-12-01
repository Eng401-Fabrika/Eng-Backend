using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.BusinessLayer.Utils;
using Eng_Backend.DtoLayer.Auth;
using Eng_Backend.DAL.Entities; // ApplicationUser is defined here
using Microsoft.AspNetCore.Identity; // Required for UserManager and SignInManager

namespace Eng_Backend.BusinessLayer.Managers;

public class AuthManager : IAuthService
{
    // Using Identity's own managers instead of GenericService
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtHelper _jwtHelper;

    public AuthManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtHelper jwtHelper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtHelper = jwtHelper;
    }

    public async Task<ServiceResult> RegisterAsync(RegisterDto dto)
    {
        // 1. Create User Object
        var user = new ApplicationUser
        {
            UserName = dto.Email, // UserName is required in Identity, we can use email
            Email = dto.Email,
            FullName = dto.FullName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // 2. Save with Identity Library (Automatically hashes the password and stores it as a string)
        // No need to use HashingHelper.
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            return ServiceResult.Ok(null, "Registration completed successfully.");
        }

        // If there are errors (e.g., password too short, email already in use, etc.)
        // We combine and return the errors
        var errorMsg = string.Join(", ", result.Errors.Select(e => e.Description));
        return ServiceResult.Fail(errorMsg);
    }

    public async Task<ServiceResult> LoginAsync(LoginDto dto)
    {
        // 1. Find User by Email
        var user = await _userManager.FindByEmailAsync(dto.Email);
        
        if (user == null)
            return ServiceResult.Fail("User not found.");

        // 2. Password Verification (Identity internally decodes the string hash and verifies)
        // No need for VerifyPasswordHash method.
        var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

        if (!checkPassword)
            return ServiceResult.Fail("Invalid password.");

        // 3. Token Generation
        // NOTE: You may need to update the JwtHelper class's CreateToken method parameter
        // to accept "ApplicationUser" instead of "User".
        string token = _jwtHelper.CreateToken(user);

        return ServiceResult.Ok(new { Token = token }, "Login successful.");
    }
}