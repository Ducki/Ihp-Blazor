using Ihp_Blazor.Models;

namespace Ihp_Blazor.Services;

public class FeedsViewService : IFeedsViewService
{
    private readonly IFeedsService _feedsService;

    public FeedsViewService(IFeedsService feedsService) => _feedsService = feedsService;

    public async Task<FeedCollectionViewModel> GetFeedsViewModel() =>
        MapToFeedCollectionViewModel(
            await _feedsService.GetFeeds());

    private static FeedCollectionViewModel MapToFeedCollectionViewModel(
        IEnumerable<LightSyndicationFeed> lightSyndicationFeeds) =>
        new()
        {
            SiteFeeds = lightSyndicationFeeds.ToList()
        };
}