namespace Logic.Documents.GetDocument;

public class GetDocumentLogic(DataAccessService dataAccessService)
    : IRequestHandler<GetDocumentInput, GetDocumentOutput>
{
    public async Task<GetDocumentOutput> Handle(GetDocumentInput input, CancellationToken cancellationToken = default)
    {
        var document = await dataAccessService.Documents
            .Where(x => x.Id == input.Id)
            .SingleAsync(cancellationToken);

        return document.Adapt<GetDocumentOutput>();
    }
}
