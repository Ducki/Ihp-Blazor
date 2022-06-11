using Ihp_Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace Ihp_Blazor.Views.Components.FeedItem;

public partial class FeedItem
{
    [Parameter] public LightSyndicationItem SyndicationItem { get; set; } = null!;
}