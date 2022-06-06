using System.ServiceModel.Syndication;
using System.Xml;
using Ihp_Blazor.Brokers;
using Ihp_Blazor.Models;

namespace Ihp_Blazor.Services;

public class FeedsService : IFeedsService
{
    private IFeedSourcesBroker _feedSourcesBroker;

    public FeedsService(IFeedSourcesBroker feedSourcesBroker) => _feedSourcesBroker = feedSourcesBroker;

    public IEnumerable<LightSyndicationFeed> GetFeeds()
    {
        var feedUrls = _feedSourcesBroker.GetFeedSources();

        var downloadJobs = feedUrls.Select(url =>
                DownloadFeedAsync(url.Url))
            .ToList();

        Task.WaitAll(downloadJobs.ToArray());

        return downloadJobs.Select(d => d.Result).ToList();
    }

    private static async Task<LightSyndicationFeed> DownloadFeedAsync(string url)
    {
        var requestUri = new Uri(url);

        Stream responseStream;
        try
        {
            responseStream = await new HttpClient().GetStreamAsync(requestUri);
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
        var xmlreader = XmlReader.Create(responseStream);
        return SyndicationFeed.Load(xmlreader);
    }
}