using Eng_Backend.DtoLayer.Navigation;

namespace Eng_Backend.BusinessLayer.Interfaces;

public interface INavigationService
{
    Task<List<NavigationGroupDto>> GetNavigationAsync(IReadOnlyCollection<string> roles);
}

