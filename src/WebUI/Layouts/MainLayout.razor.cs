using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace WebUI.Layouts;

public partial class MainLayout
{
    private bool _drawerOpen = true;
    private ErrorBoundary? _errorBoundary;

    private readonly MudTheme _theme = new()
    {
        Palette = new PaletteLight()
        {
            AppbarBackground = "#1D4580",
            Primary = "#281E52",
            DrawerBackground = "#45BD7F",
            DrawerText = "#FFFFFF",
            DrawerIcon = "#FFFFFF",
        },
        Typography = new Typography()
        {
            Default = new Default()
            {
                FontFamily = ["Barlow", "Arial", "sans-serif"]
            }
        }
    };

    private void ToggleDrawer()
    {
        _drawerOpen = !_drawerOpen;
    }

    protected override void OnParametersSet()
    {
        _errorBoundary?.Recover();
    }
}