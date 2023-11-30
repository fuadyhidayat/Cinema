namespace WebUI.Services.SimpleAuthentication.Components;

public partial class AccountInfo
{
    private void NavigateToLogout()
    {
        _navigationManager.NavigateTo("/Account/Logout", true);
    }
}
