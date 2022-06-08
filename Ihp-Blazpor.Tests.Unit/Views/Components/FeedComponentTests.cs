using Bunit;
using FluentAssertions;
using Ihp_Blazor.Models;
using Ihp_Blazor.Services;
using Ihp_Blazor.Views.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Ihp_Blazpor.Tests.Unit.Views.Components;

public class FeedComponentTests : TestContext
{
    [Fact]
    public void ShouldRenderComponentState()
    {
        // Arrange
        var fakeFeedCollectionViewModel = new FeedCollectionViewModel()
        {
            SiteFeeds = new List<LightSyndicationFeed>()
            {
                new()
                {
                    SiteName = "Foo",
                    FeedItems = new List<LightSyndicationItem>()
                    {
                        new()
                    }
                }
            }
        };

        var mockFeedsViewService = new Mock<IFeedsViewService>();
        mockFeedsViewService
            .Setup(service =>
                service.GetFeedsViewModel())
            .ReturnsAsync(fakeFeedCollectionViewModel);

        Services.AddScoped(s => mockFeedsViewService.Object);

        // Act
        var renderedComponent =
            RenderComponent<Feed>();

        // Assert
        renderedComponent.Instance.Should().NotBeNull();
        renderedComponent.Markup.Should().Contain("Foo");
        mockFeedsViewService.Verify(service => service.GetFeedsViewModel(), Times.Once);
        mockFeedsViewService.VerifyNoOtherCalls();
    }
}