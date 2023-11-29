using Logic.Common.Exceptions;
using Logic.Movies.RemoveMovie;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebUI.Features.Movies.Components;

public partial class DialogRemoveMovie
{
    [Parameter]
    public required int MovieId { get; init; }

    [Parameter]
    public required string MovieTitle { get; init; }

    [CascadingParameter]
    protected MudDialogInstance MudDialog { get; init; } = default!;

    private bool _isLoading;
    private Exception? _exception = null;

    protected void Cancel()
    {
        MudDialog.Cancel();
    }

    private async Task Confirm()
    {
        try
        {
            _isLoading = true;

            var input = new RemoveMovieInput
            {
                Id = MovieId
            };

            await _sender.Send(input);

            _isLoading = false;

            _ = _snackbar.Add($"Movie {MovieTitle} has been successfully removed.", Severity.Success);

            MudDialog.Close(DialogResult.Ok(true));
        }
        catch (Exception ex)
        {
            _exception = ex;
        }
        finally
        {
            _isLoading = false;
        }
    }
}
