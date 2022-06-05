using Ihp_Blazor.Models;
using Microsoft.Extensions.Options;

namespace Ihp_Blazor.Brokers;

public class FeedSourcesBroker
{
    private FeedSourcesOptions _feedSourcesOptions;

    public FeedSourcesBroker(FeedSourcesOptions options) => _feedSourcesOptions = options;

    public void GetFeedSources()
    {
        throw new NotImplementedException();
    }
}

public static class FeedSourcesBrokerOptionsExtension
{
    public static void AddFeedSource(this WebApplication webApplication, string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new ArgumentException("Value cannot be null or empty.", nameof(filePath));

        var feedOptions = new FeedSourcesOptions()
        {
            FilePath = filePath
        };

        webApplication.Services.GetRequiredService<IConfigureNamedOptions<FeedSourcesOptions>>()
            .Configure(feedOptions);
    }
}