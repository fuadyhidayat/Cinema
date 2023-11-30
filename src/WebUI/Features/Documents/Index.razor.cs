using Logic.Common.Extensions;
using Logic.Documents.GetDocuments;

namespace WebUI.Features.Documents;

public partial class Index
{
    private List<BreadcrumbItem> _breadcrumbItems = [];
    private bool _isLoading;
    private List<DocumentDto> _documents = [];
    private string? _searchKeyword;

    protected override async Task OnInitializedAsync()
    {
        LoadBreadcrumbs();
        await LoadDocuments();
    }

    private async Task LoadDocuments()
    {
        _isLoading = true;

        var input = new GetDocumentsInput();
        var output = await _sender.Send(input);
        _documents = (List<DocumentDto>)output.Documents;
        _isLoading = false;
    }

    private void LoadBreadcrumbs()
    {
        _breadcrumbItems =
        [
            new("Home", ""),
            new("Documents", "", disabled: true)
        ];
    }

    private bool FilterDocuments(DocumentDto document)
    {
        if (string.IsNullOrWhiteSpace(_searchKeyword))
        {
            return true;
        }

        if (document.Title.Contains(_searchKeyword, StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        if (document.FileName.Contains(_searchKeyword, StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        if (document.FileSize.ToReadableFileSize().Contains(_searchKeyword, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }
}
