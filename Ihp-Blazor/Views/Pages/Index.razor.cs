using Ihp_Blazor.Models;
using Ihp_Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace Ihp_Blazor.Views.Pages;

public partial class Index : ComponentBase
{
    [Inject] private IFeedsViewService FeedsViewService { get; set; } = null!;
    [Inject] private ILogger<Index> Logger { get; set; } = null!;
    private FeedCollectionViewModel? FeedCollectionViewModel { get; set; }

    protected override void OnInitialized() =>
        Logger.LogInformation("First initialization");


    protected override async void OnAfterRender(bool firstRender)
    {
        Logger.LogInformation("Getting new feeds after render");
        FeedCollectionViewModel = await FeedsViewService.GetFeedsViewModel();

        if (firstRender) StateHasChanged();
    }

    private void UpdateTriggered() =>
        Logger.LogInformation("Triggered update");
}