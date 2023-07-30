using Ihp_Blazor.Models;
using Ihp_Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace Ihp_Blazor.Views.Pages;

public partial class Index : ComponentBase
{
    [Inject] private IFeedsService FeedsService { get; set; } = null!;
    [Inject] private ILogger<Index> Logger { get; set; } = null!;
    private IEnumerable<LightSyndicationFeed>? Feeds { get; set; }

    protected override void OnInitialized() =>
        Logger.LogInformation("First initialization");


    protected override async void OnAfterRender(bool firstRender)
    {
        Logger.LogInformation("Getting new feeds after render");
        Feeds = await FeedsService.GetFeeds();

        if (firstRender) StateHasChanged();
    }

    private void UpdateTriggered() =>
        Logger.LogInformation("Triggered update");
}