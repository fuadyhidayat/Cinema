using Logic.Documents.RemoveDocument;

namespace WebUI.Features.Documents.Components;

public partial class DialogRemoveDocument
{
    [Parameter]
    public required int DocumentId { get; init; }

    [Parameter]
    public required string DocumentTitle { get; init; }

    [CascadingParameter]
    protected MudDialogInstance MudDialog { get; init; } = default!;

    private bool _isLoading;

    protected void Cancel()
    {
        MudDialog.Cancel();
    }

    private async Task Confirm()
    {
        _isLoading = true;

        var input = new RemoveDocumentInput
        {
            Id = DocumentId
        };

        _ = await _sender.Send(input);

        _isLoading = false;

        _ = _snackbar.Add($"Document {DocumentTitle} has been successfully removed.", Severity.Success);

        MudDialog.Close(DialogResult.Ok(true));
    }
}
