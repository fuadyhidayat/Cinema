using Logic.Services.Storage;

namespace Logic.Documents.RemoveDocument;

public class RemoveDocumentLogic(
    DataAccessService dataAccessService,
    StorageService storageService)
    : IRequestHandler<RemoveDocumentInput, RemoveDocumentOutput>
{
    public async Task<RemoveDocumentOutput> Handle(RemoveDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await dataAccessService.Documents
            .Where(x => x.Id == input.Id)
            .SingleAsync(cancellationToken);

        await storageService.DeleteAsync(document.FileCode);

        _ = dataAccessService.Documents.Remove(document);
        _ = await dataAccessService.SaveChangesAsync(cancellationToken);

        return new RemoveDocumentOutput();
    }
}
