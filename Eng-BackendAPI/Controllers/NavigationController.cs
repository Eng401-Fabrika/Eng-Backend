using System.Security.Claims;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DtoLayer.Common;
using Eng_Backend.DtoLayer.Navigation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eng_BackendAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NavigationController : ControllerBase
{
    private readonly INavigationService _navigationService;

    public NavigationController(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNavigation()
    {
        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        var navigation = await _navigationService.GetNavigationAsync(roles);

        return Ok(ApiResponse<List<NavigationGroupDto>>.SuccessResponse(navigation, "Navigation retrieved successfully"));
    }
}

