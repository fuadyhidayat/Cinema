using Logic.Services.Storage;

namespace Logic.Documents.GetDocumentFile;

public class GetDocumentFileLogic(
    DataAccessService dataAccessService,
    StorageService storageService)
    : IRequestHandler<GetDocumentFileInput, GetDocumentFileOutput>
{
    public async Task<GetDocumentFileOutput> Handle(GetDocumentFileInput input, CancellationToken cancellationToken = default)
    {
        var document = await dataAccessService.Documents
            .Where(x => x.Id == input.Id)
            .SingleAsync(cancellationToken);

        var fileContent = await storageService.ReadAsync(document.FileCode);

        return new GetDocumentFileOutput
        {
            FileContent = fileContent
        };
    }
}
