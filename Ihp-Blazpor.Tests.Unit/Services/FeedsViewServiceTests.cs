using FluentAssertions;
using Ihp_Blazor.Models;
using Ihp_Blazor.Services;
using Moq;

namespace Ihp_Blazpor.Tests.Unit.Services;

public class FeedsViewServiceTests
{
    [Fact]
    public void ShouldGetFeeds()
    {
        // Arrange

        IEnumerable<LightSyndicationFeed> fakeFeeds = new List<LightSyndicationFeed>()
        {
            new()
            {
                SiteName = "One",
                FeedItems = new List<LightSyndicationItem>()
                {
                    new Mock<LightSyndicationItem>().Object
                }
            }
        };

        IEnumerable<LightSyndicationFeed> expectedFeeds = new List<LightSyndicationFeed>()
        {
            new()
            {
                SiteName = "One",
                FeedItems = new List<LightSyndicationItem>()
                {
                    new Mock<LightSyndicationItem>().Object
                }
            }
        };

        var fakeFeedService = Mock.Of<IFeedsService>(service => service.GetFeeds() == fakeFeeds);
        var feedsViewService = new FeedsViewService(fakeFeedService);

        // Act
        var feeds = feedsViewService.GetFeedsViewModel();

        // Assert
        feeds.SiteFeeds.Should().BeEquivalentTo(expectedFeeds);
    }
}