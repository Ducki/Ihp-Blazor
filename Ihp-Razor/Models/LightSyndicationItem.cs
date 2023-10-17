namespace Ihp_Razor.Models;

public class LightSyndicationItem
{
    public string Title { get; init; } = null!;
    public string Url { get; init; } = null!;
    public string Summary { get; init; } = null!;
    public DateTime PublishDate { get; init; }
}