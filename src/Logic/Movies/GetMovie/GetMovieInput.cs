namespace Logic.Movies.GetMovie;

public record GetMovieInput : IRequest<GetMovieOutput>
{
    public required int Id { get; init; }
}

public class GetMovieInputValidator : AbstractValidator<GetMovieInput>
{
    public GetMovieInputValidator()
    {
        _ = RuleFor(x => x.Id)
            .GreaterThan(0);
    }
}