using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eng_BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet("secured")]
        public IActionResult Secured()
        {
            return Ok("Token doğrulandı, erişim başarılı!");
        }
    }
}