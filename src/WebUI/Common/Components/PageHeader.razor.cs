using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebUI.Common.Components;

public partial class PageHeader
{
    [Parameter]
    public string? BrowserTabTitle { get; init; }

    [Parameter]
    public required List<BreadcrumbItem> BreadcrumbItems { get; init; } = [];

    [Parameter]
    public string? Title { get; init; }
}