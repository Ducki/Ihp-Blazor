using Ihp_Razor.Models;

namespace Ihp_Razor.Services;

public interface IFeedsService
{
    Task<IEnumerable<LightSyndicationFeed>> GetFeeds();
}