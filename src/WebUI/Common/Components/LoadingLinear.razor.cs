using Microsoft.AspNetCore.Components;

namespace WebUI.Common.Components;

public partial class LoadingLinear
{
    [Parameter]
    public bool IsVisible { get; init; }
}