namespace Domain.Entities;

public class Document
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string FileName { get; set; }
    public required long FileSize { get; set; }
    public required string FileCode { get; set; }
}
