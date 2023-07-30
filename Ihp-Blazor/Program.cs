using Ihp_Blazor.Brokers;
using Ihp_Blazor.Models;
using Ihp_Blazor.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages(options =>
    options.RootDirectory = "/Views/Pages");
builder.Services.AddServerSideBlazor();
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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();