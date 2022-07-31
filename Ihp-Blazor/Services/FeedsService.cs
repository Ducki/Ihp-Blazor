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
                DownloadFeedAsync(url.Url))
            .ToList();

        await Task.WhenAll(downloadJobs);

        var resultFeedDataStrings = downloadJobs.Select(d => d.Result).ToList();
        var resultFeedData = resultFeedDataStrings.Select(ReadSyndicationFeed);
        return resultFeedData.Select(MapSyndicationFeedToLightSyndicationFeed);
    }

    private async Task<string> DownloadFeedAsync(string url)
    {
        var requestUri = new Uri(url);
        string responseString;
        var client = _httpClientFactory.CreateClient();

        try
        {
            responseString = await client.GetStringAsync(requestUri);
        }
        catch (Exception e)
        {
            // Try again …
            Console.WriteLine(e);
            responseString = await client.GetStringAsync(requestUri);
        }

        return responseString;
    }

    private static LightSyndicationFeed MapSyndicationFeedToLightSyndicationFeed(SyndicationFeed syndicationFeed)
    {
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

    private static SyndicationFeed ReadSyndicationFeed(string responseString)
    {
        var xmlreader = XmlReader.Create(responseString);
        return SyndicationFeed.Load(xmlreader);
    }
}