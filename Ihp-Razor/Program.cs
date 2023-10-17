using Ihp_Razor.Brokers;
using Ihp_Razor.Models;
using Ihp_Razor.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();