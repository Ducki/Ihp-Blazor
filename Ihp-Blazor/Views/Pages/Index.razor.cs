using Ihp_Blazor.Models;
using Ihp_Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace Ihp_Blazor.Views.Pages;

public partial class Index : ComponentBase
{
    [Inject] private IFeedsViewService FeedsViewService { get; set; } = null!;
    private FeedCollectionViewModel? FeedCollectionViewModel { get; set; }

    protected override async void OnInitialized()
    {
        FeedCollectionViewModel = await FeedsViewService.GetFeedsViewModel();
    }

    protected override async void OnParametersSet()
    {
        FeedCollectionViewModel = await FeedsViewService.GetFeedsViewModel();
    }

    private void UpdateTriggered()
    {
        Console.WriteLine("TRIGGERED");
    }
}