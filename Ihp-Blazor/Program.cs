using Ihp_Blazor.Brokers;
using Ihp_Blazor.Models;
using Ihp_Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
    options.RootDirectory = "/Views/Pages");
builder.Services.AddServerSideBlazor();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
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