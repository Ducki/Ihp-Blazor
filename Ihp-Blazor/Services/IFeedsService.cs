using Ihp_Blazor.Models;

namespace Ihp_Blazor.Services;

public interface IFeedsService
{
    IEnumerable<LightSyndicationFeed> GetFeeds();
}