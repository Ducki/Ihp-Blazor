using Ihp_Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace Ihp_Blazor.Views.Components;

public partial class Feed : ComponentBase
{
    [Parameter] public FeedCollectionViewModel? FeedCollectionViewModel { get; set; }
}