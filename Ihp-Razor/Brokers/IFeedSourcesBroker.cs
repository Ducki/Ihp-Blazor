using Ihp_Razor.Models;

namespace Ihp_Razor.Brokers;

public interface IFeedSourcesBroker
{
    IEnumerable<JsonUrl> GetFeedSources();
}