namespace Logic.Movies.RemoveMovie;

public class RemoveMovieLogic(DataAccessService dataAccessService)
    : IRequestHandler<RemoveMovieInput, RemoveMovieOutput>
{
    public async Task<RemoveMovieOutput> Handle(RemoveMovieInput input, CancellationToken cancellationToken = default)
    {
        if (input.Id % 2 == 0)
        {
            throw new Exception($"Movie dengan ID {input.Id} tidak boleh dihapus karena sudah ada pelanggan yang membeli tiketnya.");
        }

        var movie = await dataAccessService.Movies
            .Where(x => x.Id == input.Id)
            .SingleAsync(cancellationToken);

        _ = dataAccessService.Movies.Remove(movie);
        _ = await dataAccessService.SaveChangesAsync(cancellationToken);

        return new RemoveMovieOutput();
    }
}
