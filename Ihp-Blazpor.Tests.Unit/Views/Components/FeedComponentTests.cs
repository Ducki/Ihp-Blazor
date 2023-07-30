using Bunit;
using FluentAssertions;
using Ihp_Blazor.Models;
using Ihp_Blazor.Views.Components.Feed;

namespace Ihp_Blazpor.Tests.Unit.Views.Components;

public class FeedComponentTests : TestContext
{
    [Fact]
    public void ShouldRenderFeedComponentState()
    {
        // Arrange
        var fakeFeedCollectionViewModel =
                new List<LightSyndicationFeed>
                {
                    new()
                    {
                        SiteName = "Foo",
                        FeedItems = new List<LightSyndicationItem>
                        {
                            new()
                        }
                    }
                }
            ;

        // Act
        var renderedComponent =
            RenderComponent<Feed>(parameterBuilder =>
                parameterBuilder.Add(param =>
                    param.Feeds, fakeFeedCollectionViewModel));

        // Assert
        renderedComponent.Instance.Should().NotBeNull();
        renderedComponent.WaitForAssertion(() =>
                renderedComponent.Markup.Should().Contain("Foo"),
            TimeSpan.FromSeconds(2));
    }
}