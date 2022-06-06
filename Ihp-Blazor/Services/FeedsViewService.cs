using Ihp_Blazor.Models;

namespace Ihp_Blazor.Services;

public class FeedsViewService : IFeedsViewService
{
    private readonly FeedsService _feedsService;

    public FeedsViewService(FeedsService feedsService) => _feedsService = feedsService;

    public FeedCollectionViewModel GetFeeds() => MapToFeedCollectionViewModel(_feedsService.GetFeeds());

    private static FeedCollectionViewModel MapToFeedCollectionViewModel(
        IEnumerable<LightSyndicationFeed> lightSyndicationFeeds) =>
        new()
        {
            SiteFeeds = lightSyndicationFeeds.ToList()
        };
}