using Logic;
using Logic.Services.Logging;
using MudBlazor.Services;
using WebUI.Services.CurrentUser;
using WebUI.Services.SimpleAuthentication;
using WebUI.Services.SimpleAuthorization;
using WebUI.Services.SimpleUserProfile;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLogging();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLogic(builder.Configuration);
builder.Services.AddMudServices();
builder.Services.AddSimpleAuthentication();
builder.Services.AddSimpleUserProfile();
builder.Services.AddSimpleAuthorization();
builder.Services.AddCurrentUser();

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
