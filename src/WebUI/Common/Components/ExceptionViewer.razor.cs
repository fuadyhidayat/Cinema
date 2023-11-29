using Logic.Common.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebUI.Common.Components;

public partial class ExceptionViewer
{
    [Parameter]
    public required Exception? Exception { get; init; }

    private IReadOnlyCollection<string> _errorMessages = Array.Empty<string>();

    protected override void OnParametersSet()
    {
        if (Exception is null)
        {
            return;
        }

        _errorMessages = Exception.GetAllErrorMessages();

        foreach (var errorMessage in _errorMessages)
        {
            _snackbar.Add(errorMessage, Severity.Error);
        }
    }
}