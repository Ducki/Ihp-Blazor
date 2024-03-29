using Microsoft.AspNetCore.Components;

namespace Ihp_Blazor.Views.Components.FeedUpdate;

public partial class FeedUpdate : ComponentBase
{
    [Parameter] public EventCallback TriggerUpdate { get; set; }
    private DateTimeOffset LastUpdateTime { get; set; }

    protected override Task OnInitializedAsync()
    {
        LastUpdateTime = GetCurrentDateTimeOffset();
        return Task.CompletedTask;
    }

    private void UpdateFeeds()
    {
        Console.WriteLine(value: "Clicked on Update Feeds");
        TriggerUpdate.InvokeAsync();

        LastUpdateTime = GetCurrentDateTimeOffset();
    }

    private static DateTimeOffset GetCurrentDateTimeOffset() => DateTimeOffset.Now;
}