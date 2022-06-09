namespace Ihp_Blazor.Models;

public class LightSyndicationFeed
{
    public string SiteName { get; init; } = null!;
    public List<LightSyndicationItem> FeedItems { get; init; } = null!;
}