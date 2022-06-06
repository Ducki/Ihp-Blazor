using Ihp_Blazor.Brokers;
using Ihp_Blazor.Models;
using Ihp_Blazor.Services;

namespace Ihp_Blazor.DependencyInjection;

public static class ServiceRegistrations
{
    public static void AddIhpServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IFeedSourcesBroker>(provider =>
        {
            var options = new FeedSourcesOptions()
            {
                FilePath = builder.Configuration.GetValue<string>("FeedSourceFilePath")
            };

            return new FeedSourcesBroker(options);
        });
        builder.Services.AddScoped<IFeedsService, FeedsService>();
        builder.Services.AddScoped<IFeedsViewService, FeedsViewService>();
    }
}