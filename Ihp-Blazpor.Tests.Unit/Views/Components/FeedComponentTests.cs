using Bunit;
using FluentAssertions;
using Ihp_Blazor.Views.Components;

namespace Ihp_Blazpor.Tests.Unit.Views.Components;

public class FeedComponentTests : TestContext
{
    [Fact]
    public void ShouldRenderComponentState()
    {
        // Arrange

        // Act
        var renderedComponent = RenderComponent<FeedItem>();

        // Assert
        renderedComponent.Instance.Should().NotBeNull();
    }
}