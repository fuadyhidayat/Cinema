using System.Text.Json.Serialization;
using Logic.Documents.Constants;

namespace Logic.Documents.AddDocument;

public record AddDocumentInput : IRequest<AddDocumentOutput>
{
    public required string Title { get; init; }
    public required string FileName { get; init; }

    [JsonIgnore]
    public byte[] FileContent { get; set; } = default!;
}

public class AddDocumentInputValidator : AbstractValidator<AddDocumentInput>
{
    public AddDocumentInputValidator()
    {
        _ = RuleFor(x => x.Title)
            .MinimumLength(MinimumLengthFor.Title)
            .MaximumLength(MaximumLengthFor.Title);

        _ = RuleFor(x => x.FileName)
            .MinimumLength(MinimumLengthFor.FileName)
            .MaximumLength(MaximumLengthFor.FileName);
    }
}
