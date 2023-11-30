namespace WebUI.Services.SimpleAuthentication.Components;

public partial class RedirectToLogin
{
    protected override void OnAfterRender(bool firstRender)
    {
        _navigationManager.NavigateTo($"Account/Login?returnUrl={Uri.EscapeDataString(_navigationManager.Uri)}", true);
    }
}
