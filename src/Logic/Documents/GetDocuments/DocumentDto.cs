namespace Logic.Documents.GetDocuments;

public record DocumentDto
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required string FileName { get; set; }
    public required long FileSize { get; set; }
}
