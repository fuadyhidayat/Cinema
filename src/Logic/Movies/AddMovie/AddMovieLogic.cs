namespace Logic.Movies.AddMovie;

public class AddMovieLogic(DataAccessService dataAccessService)
    : IRequestHandler<AddMovieInput, AddMovieOutput>
{
    public async Task<AddMovieOutput> Handle(AddMovieInput input, CancellationToken cancellationToken = default)
    {
        var movie = input.Adapt<Movie>();

        _ = await dataAccessService.Movies.AddAsync(movie, cancellationToken);
        _ = await dataAccessService.SaveChangesAsync(cancellationToken);

        return new AddMovieOutput
        {
            Id = movie.Id
        };
    }
}
