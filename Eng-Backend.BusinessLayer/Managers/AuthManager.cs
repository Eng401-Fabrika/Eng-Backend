using Eng_Backend.BusinessLayer.Constants;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.BusinessLayer.Utils;
using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.DtoLayer.Auth;
using Eng_Backend.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Eng_Backend.BusinessLayer.Managers;

public class AuthManager : IAuthService
{
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
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new BadRequestException(ErrorMessages.EmailRequired);

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new BadRequestException(ErrorMessages.PasswordRequired);

            if (string.IsNullOrWhiteSpace(dto.FullName))
                throw new BadRequestException(ErrorMessages.FullNameRequired);

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new BadRequestException(ErrorMessages.UserAlreadyExists);

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(ErrorMessages.RegistrationFailed, errors);
            }

            return ServiceResult.Ok(null, SuccessMessages.RegistrationSuccessful);
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    public async Task<ServiceResult> LoginAsync(LoginDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new BadRequestException(ErrorMessages.EmailRequired);

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new BadRequestException(ErrorMessages.PasswordRequired);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new UnauthorizedException(ErrorMessages.InvalidCredentials);

            var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!checkPassword)
                throw new UnauthorizedException(ErrorMessages.InvalidCredentials);

            var roles = await _userManager.GetRolesAsync(user);

            string token = _jwtHelper.CreateToken(user, roles);

            return ServiceResult.Ok(new { Token = token }, SuccessMessages.LoginSuccessful);
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (UnauthorizedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }
}