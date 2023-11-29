namespace Logic.Movies.GetMovie;

public class GetMovieLogic(DataAccessService dataAccessService)
    : IRequestHandler<GetMovieInput, GetMovieOutput>
{
    public async Task<GetMovieOutput> Handle(GetMovieInput input, CancellationToken cancellationToken = default)
    {
        var movie = await dataAccessService.Movies
            .Where(x => x.Id == input.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (movie == null)
        {
            throw new Exception($"Movie dengan ID {input.Id} tidak ditemukan.");
        }

        return movie.Adapt<GetMovieOutput>();
    }
}
