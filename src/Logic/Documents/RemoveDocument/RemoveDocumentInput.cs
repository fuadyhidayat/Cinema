namespace Logic.Documents.RemoveDocument;

public record RemoveDocumentInput : IRequest<RemoveDocumentOutput>
{
    public required int Id { get; set; }
}

public class RemoveDocumentInputValidator : AbstractValidator<RemoveDocumentInput>
{
    public RemoveDocumentInputValidator()
    {
        _ = RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
