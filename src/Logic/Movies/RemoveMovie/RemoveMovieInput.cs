namespace Logic.Movies.RemoveMovie;

public record RemoveMovieInput : IRequest<RemoveMovieOutput>
{
    public required int Id { get; set; }
}

public class RemoveMovieInputValidator : AbstractValidator<RemoveMovieInput>
{
    public RemoveMovieInputValidator()
    {
        _ = RuleFor(x => x.Id)
            .GreaterThan(0);

        _ = RuleFor(x => x.Id)
            .NotEqual(7);
    }
}