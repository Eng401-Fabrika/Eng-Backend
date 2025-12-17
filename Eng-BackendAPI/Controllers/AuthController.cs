using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DtoLayer.Auth;
using Eng_Backend.DtoLayer.Common;
using Microsoft.AspNetCore.Mvc;

namespace Eng_BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // Service layer will throw exceptions if validation fails
            // Middleware will catch and handle them automatically
            var result = await _authService.RegisterAsync(dto);
            
            return Ok(ApiResponse.SuccessResponse(result.Message, 201));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            // Service layer will throw exceptions if validation fails
            // Middleware will catch and handle them automatically
            var result = await _authService.LoginAsync(dto);
            
            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }
    }
}