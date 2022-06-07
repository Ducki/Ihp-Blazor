using Bunit;
using FluentAssertions;
using Ihp_Blazor.Models;
using Ihp_Blazor.Views.Components;
using Moq;

namespace Ihp_Blazpor.Tests.Unit.Views.Components;

public class FeedItemComponentTests : TestContext
{
    [Fact]
    public void ShouldRenderComponentState()
    {
        // Arrange
        var syndicationFeedItem = Mock.Of<LightSyndicationItem>();
        // Act
        var renderedComponent = RenderComponent<FeedItem>(builder =>
            builder.Add(item => item.SyndicationItem, syndicationFeedItem));

        // Assert
        renderedComponent.Instance.Should().NotBeNull();
    }
}