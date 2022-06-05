using Ihp_Blazor.Brokers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
    options.RootDirectory = "/Views/Pages");
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<FeedSourcesBroker>();

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

app.AddFeedSource(app.Configuration.GetValue<string>("FeedSourceFilePath"));
app.Run();