namespace Logic.Movies.RemoveMovie;

public class RemoveMovieLogic(DataAccessService dataAccessService)
    : IRequestHandler<RemoveMovieInput, RemoveMovieOutput>
{
    public async Task<RemoveMovieOutput> Handle(RemoveMovieInput input, CancellationToken cancellationToken = default)
    {
        var movie = await dataAccessService.Movies
            .Where(x => x.Id == input.Id)
            .SingleAsync(cancellationToken);

        _ = dataAccessService.Movies.Remove(movie);
        _ = await dataAccessService.SaveChangesAsync(cancellationToken);

        return new RemoveMovieOutput();
    }
}
