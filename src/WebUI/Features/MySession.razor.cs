namespace WebUI.Features;

public partial class MySession
{
    private List<BreadcrumbItem> _breadcrumbItems = [];

    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    private void LoadBreadcrumbs()
    {
        _breadcrumbItems =
        [
            new("Home", ""),
            new("My Session", "", disabled: true)
        ];
    }
}
