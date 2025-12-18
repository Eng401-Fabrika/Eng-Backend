namespace Eng_Backend.DtoLayer.Navigation;

public class NavigationItemDto
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public int Order { get; set; }
}

public class NavigationGroupDto
{
    public string Id { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<NavigationItemDto> Items { get; set; } = new();
}

