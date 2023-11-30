namespace Logic.Documents.GetDocument;

public record GetDocumentInput : IRequest<GetDocumentOutput>
{
    public required int Id { get; init; }
}

public class GetDocumentInputValidator : AbstractValidator<GetDocumentInput>
{
    public GetDocumentInputValidator()
    {
        _ = RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
