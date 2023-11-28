using Logic.Services.DataAccess;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var connectionString = builder.Configuration["DataAccess:ConnectionString"];

builder.Services.AddDbContext<DataAccessService>(options =>
{
    _ = options.UseSqlServer(connectionString, builder =>
    {
        _ = builder.MigrationsAssembly(typeof(DataAccessService).Assembly.FullName);
    });
});

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
