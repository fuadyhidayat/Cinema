using Logic.Services.Storage;

namespace Logic.Documents.AddDocument;

public class AddDocumentLogic(
    DataAccessService dataAccessService,
    StorageService storageService)
    : IRequestHandler<AddDocumentInput, AddDocumentOutput>
{
    public async Task<AddDocumentOutput> Handle(AddDocumentInput input, CancellationToken cancellationToken = default)
    {
        var fileCode = await storageService.CreateAsync(input.FileContent);

        var document = new Document
        {
            Title = input.Title,
            FileCode = fileCode,
            FileSize = input.FileContent.Length,
            FileName = input.FileName
        };

        _ = await dataAccessService.Documents.AddAsync(document, cancellationToken);
        _ = await dataAccessService.SaveChangesAsync(cancellationToken);

        return new AddDocumentOutput
        {
            Id = document.Id
        };
    }
}
