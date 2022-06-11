using Ihp_Blazor.DependencyInjection;
using Tailwind;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
    options.RootDirectory = "/Views/Pages");
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();

builder.AddIhpServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
    app.RunTailwind("tailwind", "./");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();