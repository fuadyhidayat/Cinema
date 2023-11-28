namespace Logic.Movies.GetMovies;

public record GetMoviesInput : IRequest<GetMoviesOutput>
{
    public float MinimumRating { get; init; } = 0.0f;
}
