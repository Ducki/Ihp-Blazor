namespace Ihp_Blazor.Models;

public class LightSyndicationItem
{
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public DateTime PublishDate { get; set; }
}