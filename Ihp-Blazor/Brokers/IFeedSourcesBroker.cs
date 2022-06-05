using Ihp_Blazor.Models;

namespace Ihp_Blazor.Brokers;

public interface IFeedSourcesBroker
{
    IEnumerable<JsonUrl> GetFeedSources();
}