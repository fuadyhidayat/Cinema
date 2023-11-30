namespace Logic.Documents.GetDocuments;

public class GetDocumentsLogic(DataAccessService dataAccessService)
    : IRequestHandler<GetDocumentsInput, GetDocumentsOutput>
{
    public async Task<GetDocumentsOutput> Handle(GetDocumentsInput input, CancellationToken cancellationToken = default)
    {
        var documents = await dataAccessService.Documents
            .ToListAsync(cancellationToken);

        return new GetDocumentsOutput
        {
            Documents = documents.Adapt<List<DocumentDto>>()
        };
    }
}
