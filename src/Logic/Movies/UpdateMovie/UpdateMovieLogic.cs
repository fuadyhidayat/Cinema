namespace Logic.Movies.UpdateMovie;

public class UpdateMovieLogic(DataAccessService dataAccessService)
    : IRequestHandler<UpdateMovieInput, UpdateMovieOutput>
{
    public async Task<UpdateMovieOutput> Handle(UpdateMovieInput input, CancellationToken cancellationToken = default)
    {
        var movie = await dataAccessService.Movies
            .Where(x => x.Id == input.Id)
            .SingleAsync(cancellationToken);

        movie.Title = input.Title;
        movie.Synopsis = input.Synopsis;
        movie.ReleaseDate = input.ReleaseDate;
        movie.Rating = input.Rating;
        movie.TicketPrice = input.TicketPrice;

        _ = await dataAccessService.SaveChangesAsync(cancellationToken);

        return new UpdateMovieOutput();
    }
}
