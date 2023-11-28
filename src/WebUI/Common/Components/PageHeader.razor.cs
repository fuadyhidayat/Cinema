using Microsoft.AspNetCore.Components;

namespace WebUI.Common.Components;

public partial class PageHeader
{
    [Parameter]
    public required string Title { get; init; }
}