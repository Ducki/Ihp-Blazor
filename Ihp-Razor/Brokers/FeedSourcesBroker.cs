using System.Text.Json;
using Ihp_Razor.Models;

namespace Ihp_Razor.Brokers;

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
    public static void AddFeedSource(this WebApplicationBuilder builder, Action<FeedSourcesOptions> options) =>
        builder.Services.AddOptions<FeedSourcesOptions>().Configure(options);
}