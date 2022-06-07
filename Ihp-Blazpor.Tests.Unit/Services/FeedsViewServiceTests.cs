using FluentAssertions;
using Ihp_Blazor.Models;
using Ihp_Blazor.Services;
using Moq;

namespace Ihp_Blazpor.Tests.Unit.Services;

public class FeedsViewServiceTests
{
    [Fact]
    public async void ShouldGetFeeds()
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

        var fakeFeedService = new Mock<IFeedsService>();
        fakeFeedService
            .Setup(service => service.GetFeeds())
            .ReturnsAsync(fakeFeeds);
        var feedsViewService = new FeedsViewService(fakeFeedService.Object);

        // Act
        var feeds = await feedsViewService.GetFeedsViewModel();

        // Assert
        feeds.SiteFeeds.Should().BeEquivalentTo(expectedFeeds);
    }
}