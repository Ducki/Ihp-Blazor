using FluentAssertions;
using Ihp_Blazor.Brokers;
using Ihp_Blazor.Models;

namespace Ihp_Blazpor.Tests.Unit.Brokers;

public class FeedSourceBrokerTests
{
    [Fact]
    public void ShouldGetFeedUrls()
    {
        // Arrange
        var expectedUrls = new List<JsonUrl>
        {
            new() { Url = "url1" },
            new() { Url = "url2" }
        };

        var brokerOptions = new FeedSourcesOptions
        {
            FilePath = "Data/sources.json"
        };

        var brokerUnderTest = new FeedSourcesBroker(brokerOptions);

        // Act
        var urls = brokerUnderTest.GetFeedSources();

        // Assert
        urls.Should().BeEquivalentTo(expectedUrls);
    }
}