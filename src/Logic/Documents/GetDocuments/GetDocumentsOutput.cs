namespace Logic.Documents.GetDocuments;

public record GetDocumentsOutput
{
    public required IReadOnlyCollection<DocumentDto> Documents { get; init; } = new List<DocumentDto>();
}
