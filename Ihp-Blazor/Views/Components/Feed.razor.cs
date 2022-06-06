using Ihp_Blazor.Models;
using Ihp_Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace Ihp_Blazor.Views.Components;

public partial class Feed : ComponentBase
{
    [Inject] private IFeedsViewService FeedsViewService { get; set; } = null!;
    private FeedCollectionViewModel? FeedCollectionViewModel { get; set; }


    protected override async Task OnInitializedAsync()
    {
        FeedCollectionViewModel = await FeedsViewService.GetFeedsViewModel();
    }
}