using System.ServiceModel.Syndication;
using System.Xml;
using Ihp_Razor.Brokers;
using Ihp_Razor.Models;

namespace Ihp_Razor.Services;

public class FeedsService(
    IFeedSourcesBroker feedSourcesBroker,
    IHttpClientFactory httpClientFactory)
    : IFeedsService
{
    public async Task<IEnumerable<LightSyndicationFeed?>> GetFeeds()
    {
        var feedUrls = feedSourcesBroker.GetFeedSources();

        var downloadJobs = feedUrls.Select(url =>
                DownloadFeedAsync(url.Url))
            .ToList();

        await Task.WhenAll(downloadJobs);

        return downloadJobs.Select(d => d.Result).ToList();
    }

    private async Task<LightSyndicationFeed?> DownloadFeedAsync(string url)
    {
        var requestUri = new Uri(url);
        Stream responseStream;

        try
        {
            var client = httpClientFactory.CreateClient();
            responseStream = await client.GetStreamAsync(requestUri);
        }
        catch (Exception e)
        {
            return null;
        }

        var syndicationFeed = ReadSyndicationFeed(responseStream);
        var feedItems = syndicationFeed.Items.Take(8);

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

    private static SyndicationFeed ReadSyndicationFeed(Stream responseStream)
    {
        var xmlreader = XmlReader.Create(responseStream);
        return SyndicationFeed.Load(xmlreader);
    }
}