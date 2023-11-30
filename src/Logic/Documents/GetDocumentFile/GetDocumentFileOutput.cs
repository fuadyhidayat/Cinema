namespace Logic.Documents.GetDocumentFile;

public record GetDocumentFileOutput
{
    public required byte[] FileContent { get; init; }
}
