using Ihp_Blazor.Models;
using Ihp_Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace Ihp_Blazor.Views.Pages;

public partial class Index : ComponentBase
{
    [Inject] private IFeedsService FeedsService { get; set; } = null!;
    [Inject] private ILogger<Index> Logger { get; set; } = null!;
    private IEnumerable<LightSyndicationFeed>? Feeds;

    protected override async Task OnInitializedAsync()
    {
        Logger.LogInformation("First initialization");
        await Task.Delay(1500);
        Feeds = new List<LightSyndicationFeed>();
        //Feeds = await FeedsService.GetFeeds();
    }


    private void UpdateTriggered() =>
        Logger.LogInformation("Triggered update");
}