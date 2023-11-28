namespace Logic.Movies.GetMovies;

public record GetMoviesOutput
{
    public required IReadOnlyCollection<MovieDto> Movies { get; init; } = new List<MovieDto>();
}
