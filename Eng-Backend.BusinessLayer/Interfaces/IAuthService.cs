using Eng_Backend.DtoLayer.Auth;
using Eng_Backend.BusinessLayer.Utils;

namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IAuthService
{
    Task<ServiceResult> RegisterAsync(RegisterDto dto);
    Task<ServiceResult> LoginAsync(LoginDto dto);
}