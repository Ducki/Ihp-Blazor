using Ihp_Razor.Models;
using Ihp_Razor.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ihp_Razor.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IFeedsService _feedsService;
    public IEnumerable<LightSyndicationFeed?> Feeds { get; set; } = new List<LightSyndicationFeed?>();

    public IndexModel(ILogger<IndexModel> logger, IFeedsService feedsService)
    {
        _logger = logger;
        _feedsService = feedsService;
    }

    public async Task OnGet()
    {
        Feeds = await _feedsService.GetFeeds();
    }
}