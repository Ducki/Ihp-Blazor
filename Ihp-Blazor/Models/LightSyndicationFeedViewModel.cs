namespace Ihp_Blazor.Models;

public class LightSyndicationFeed
{
    public string SiteName { get; set; } = null!;
    public List<LightSyndicationItem> FeedItems { get; set; } = null!;
}