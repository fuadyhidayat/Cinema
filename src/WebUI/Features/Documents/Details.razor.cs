using Logic.Documents.GetDocument;
using Logic.Documents.GetDocumentFile;
using Microsoft.JSInterop;
using WebUI.Features.Documents.Components;

namespace WebUI.Features.Documents;

public partial class Details
{
    [Parameter]
    public int Id { get; set; }

    private List<BreadcrumbItem> _breadcrumbItems = [];
    private GetDocumentOutput _document = default!;
    private bool _isLoading;

    protected override async Task OnInitializedAsync()
    {
        await LoadDocument();
        LoadBreadcrumbs();
    }

    private async Task LoadDocument()
    {
        _isLoading = true;

        var input = new GetDocumentInput
        {
            Id = Id
        };

        _document = await _sender.Send(input);

        _isLoading = false;
    }

    private void LoadBreadcrumbs()
    {
        _breadcrumbItems =
        [
            new("Home", ""),
            new("Documents", "Documents"),
            new(_document.Title, "", disabled: true)
        ];
    }

    private async Task Download()
    {
        var input = new GetDocumentFileInput
        {
            Id = _document.Id
        };

        var output = await _sender.Send(input);

        await _jsRuntime.InvokeVoidAsync(
            "hahaHehe",
            _document.FileName,
            output.FileContent,
            "Holaaaaa");
    }

    private async Task ShowDialogRemoveDocument()
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogRemoveDocument.DocumentId), _document.Id },
            { nameof(DialogRemoveDocument.DocumentTitle), _document.Title }
        };

        var dialog = _dialogService.Show<DialogRemoveDocument>("Remove Document", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            _navigationManager.NavigateTo("Documents");
        }
    }
}
