using Ihp_Blazor.Models;

namespace Ihp_Blazor.Services;

public interface IFeedsViewService
{
    IEnumerable<LightSyndicationFeed> GetFeeds();
}