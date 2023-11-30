using Logic.Common.Exceptions;
using Logic.Documents.AddDocument;
using Logic.Documents.Constants;
using Microsoft.AspNetCore.Components.Forms;

namespace WebUI.Features.Documents;

public partial class Add
{
    private List<BreadcrumbItem> _breadcrumbItems = [];
    private bool _isLoading;

    private readonly AddModel _model = new()
    {
        Title = string.Empty,
        File = null
    };

    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    private void OnFileSelected(InputFileChangeEventArgs eventArgs)
    {
        _model.File = eventArgs.File;
    }

    private void LoadBreadcrumbs()
    {
        _breadcrumbItems =
        [
            new("Home", ""),
            new("Documents", "Documents"),
            new("Add", "", disabled: true)
        ];
    }

    public async Task Submit()
    {
        try
        {
            _isLoading = true;

            await using var fileStream = new MemoryStream();
            await _model.File!.OpenReadStream(MaximumValueFor.FileSize).CopyToAsync(fileStream);

            var input = new AddDocumentInput
            {
                Title = _model.Title,
                FileName = _model.File.Name,
                FileContent = fileStream.ToArray()
            };

            var output = await _sender.Send(input);

            _ = _snackbar.Add($"Document {_model.Title} has been successfully added.", Severity.Success);

            _navigationManager.NavigateTo($"Documents/Details/{output.Id}");
        }
        catch (AppValidationException ave)
        {
            _ = _snackbar.Add(ave.Message, Severity.Error);

            foreach (var errorMessage in ave.ErrorMessages)
            {
                _ = _snackbar.Add(errorMessage, Severity.Error);
            }
        }
        catch (Exception ex)
        {
            _ = _snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            _isLoading = false;
        }
    }
}

public record AddModel
{
    public required string Title { get; set; }
    public required IBrowserFile? File { get; set; }
}
