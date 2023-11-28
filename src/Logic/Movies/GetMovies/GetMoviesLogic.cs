namespace Logic.Movies.GetMovies;

public class GetMoviesLogic(DataAccessService dataAccessService)
    : IRequestHandler<GetMoviesInput, GetMoviesOutput>
{
    public async Task<GetMoviesOutput> Handle(GetMoviesInput input, CancellationToken cancellationToken = default)
    {
        var movies = await dataAccessService.Movies
            .Where(x => x.Rating >= input.MinimumRating)
            .ToListAsync(cancellationToken);

        return new GetMoviesOutput
        {
            Movies = movies.Adapt<List<MovieDto>>()
        };
    }
}
