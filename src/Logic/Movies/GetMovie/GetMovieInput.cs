namespace Logic.Movies.GetMovie;

public record GetMovieInput : IRequest<GetMovieOutput>
{
    public required int Id { get; init; }
}
