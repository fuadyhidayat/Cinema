namespace Logic.Documents.GetDocumentFile;

public record GetDocumentFileInput : IRequest<GetDocumentFileOutput>
{
    public required int Id { get; init; }
}

public class GetDocumentFileInputValidator : AbstractValidator<GetDocumentFileInput>
{
    public GetDocumentFileInputValidator()
    {
        _ = RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}
