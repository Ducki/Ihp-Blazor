using System.ServiceModel.Syndication;
using System.Xml;
using Ihp_Blazor.Brokers;
using Ihp_Blazor.Models;

namespace Ihp_Blazor.Services;

public class FeedsService : IFeedsService
{
    private readonly IFeedSourcesBroker _feedSourcesBroker;
    private readonly IHttpClientFactory _httpClientFactory;

    public FeedsService(IFeedSourcesBroker feedSourcesBroker, IHttpClientFactory httpClientFactory)
    {
        _feedSourcesBroker = feedSourcesBroker;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<LightSyndicationFeed>> GetFeeds()
    {
        var feedUrls = _feedSourcesBroker.GetFeedSources();

        var downloadJobs = feedUrls.Select(url =>
                DownloadFeedAsync(url: url.Url))
            .ToList();

        await Task.WhenAll(tasks: downloadJobs);

        return downloadJobs.Select(d => d.Result).ToList();
    }

    private async Task<LightSyndicationFeed> DownloadFeedAsync(string url)
    {
        var requestUri = new Uri(uriString: url);
        string responseStream;
        var client = _httpClientFactory.CreateClient();

        try
        {
            responseStream = await client.GetStringAsync(requestUri: requestUri);
        }
        catch (Exception e)
        {
            // Try again …
            Console.WriteLine(value: e);
            responseStream = await client.GetStringAsync(requestUri: requestUri);
        }

        var syndicationFeed = ReadSyndicationFeed(responseStream: responseStream);
        var feedItems = syndicationFeed.Items.Take(count: 8);

        var lightSyndicationItems = feedItems.Select(i => new LightSyndicationItem
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

    private static SyndicationFeed ReadSyndicationFeed(string responseStream)
    {
        var xmlreader = XmlReader.Create(inputUri: responseStream);
        return SyndicationFeed.Load(reader: xmlreader);
    }
}