using Logic;
using Logic.Services.Logging;
using MudBlazor.Services;
using WebUI.Services.SimpleAuthentication;
using WebUI.Services.SimpleUserProfile;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLogging();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLogic(builder.Configuration);
builder.Services.AddMudServices();
builder.Services.AddSimpleAuthentication();
builder.Services.AddSimpleUserProfile();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
