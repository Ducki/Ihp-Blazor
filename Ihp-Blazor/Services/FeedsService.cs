using System.ServiceModel.Syndication;
using System.Xml;
using Ihp_Blazor.Brokers;
using Ihp_Blazor.Models;

namespace Ihp_Blazor.Services;

public class FeedsService : IFeedsService
{
    private IFeedSourcesBroker _feedSourcesBroker;
    private readonly IHttpClientFactory _httpClientFactory;

    public FeedsService(IFeedSourcesBroker feedSourcesBroker, IHttpClientFactory httpClientFactory)
    {
        _feedSourcesBroker = feedSourcesBroker;
        _httpClientFactory = httpClientFactory;
    }

    public IEnumerable<LightSyndicationFeed> GetFeeds()
    {
        var feedUrls = _feedSourcesBroker.GetFeedSources();

        var downloadJobs = feedUrls.Select(url =>
                DownloadFeedAsync(url.Url))
            .ToList();
        Console.WriteLine($"After building download jobs list");
        // Task.WaitAll(downloadJobs.ToArray(), 500);
        Console.WriteLine($"After waiting jobs list");
        return downloadJobs.Select(d => d.Result).ToList();
    }

    private async Task<LightSyndicationFeed> DownloadFeedAsync(string url)
    {
        var requestUri = new Uri(url);
        Console.WriteLine($"Downloading Feed {url}");
        Stream responseStream;
        try
        {
            Console.WriteLine($"Before GetAtreamAsync / {url}");
            var client = _httpClientFactory.CreateClient();
            var foo = await client.GetStringAsync(url);
            Console.WriteLine(foo);
            responseStream = await client.GetStreamAsync(requestUri);
            Console.WriteLine($"After GetAtreamAsync {url}");
        }
        catch (Exception e)
        {
            // Try again …
            Console.WriteLine(e);
            responseStream = await new HttpClient().GetStreamAsync(requestUri);
        }

        var syndicationFeed = ReadSyndicationFeed(responseStream);
        var feedItems = syndicationFeed.Items.Take(8);

        var lightSyndicationItems = feedItems.Select(i => new LightSyndicationItem()
        {
            Title = i.Title.Text,
            PublishDate = i.PublishDate.DateTime,
            Summary = i.Summary.Text,
            Url = i.Links.First().Uri.ToString()
        });

        return new LightSyndicationFeed()
        {
            FeedItems = lightSyndicationItems.ToList(),
            SiteName = syndicationFeed.Title.Text
        };
    }

    private static SyndicationFeed ReadSyndicationFeed(Stream responseStream)
    {
        Console.WriteLine($"Reading XML ");
        var xmlreader = XmlReader.Create(responseStream);
        return SyndicationFeed.Load(xmlreader);
    }
}