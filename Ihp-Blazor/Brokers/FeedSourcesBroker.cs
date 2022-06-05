using System.Text.Json;
using Ihp_Blazor.Models;
using Microsoft.Extensions.Options;

namespace Ihp_Blazor.Brokers;

public class FeedSourcesBroker : IFeedSourcesBroker
{
    private readonly FeedSourcesOptions _feedSourcesOptions;

    public FeedSourcesBroker(FeedSourcesOptions options) => _feedSourcesOptions = options;

    public IEnumerable<JsonUrl> GetFeedSources()
    {
        var fileContent = File.ReadAllText(_feedSourcesOptions.FilePath);
        return JsonSerializer.Deserialize<List<JsonUrl>>(fileContent)
               ?? throw new InvalidOperationException();
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