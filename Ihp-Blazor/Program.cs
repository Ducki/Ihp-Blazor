using Ihp_Blazor;
using Ihp_Blazor.Brokers;
using Ihp_Blazor.Models;
using Ihp_Blazor.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IFeedSourcesBroker>(_ =>
{
    var options = new FeedSourcesOptions
    {
        FilePath = builder.Configuration.GetValue<string>("FeedSourceFilePath")!
    };

    return new FeedSourcesBroker(options);
});

builder.Services.AddScoped<IFeedsService, FeedsService>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();