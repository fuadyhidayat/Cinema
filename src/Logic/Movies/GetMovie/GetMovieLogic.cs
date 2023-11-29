using Microsoft.Extensions.Logging;

namespace Logic.Movies.GetMovie;

public class GetMovieLogic(DataAccessService dataAccessService)
    : IRequestHandler<GetMovieInput, GetMovieOutput>
{
    public async Task<GetMovieOutput> Handle(GetMovieInput input, CancellationToken cancellationToken = default)
    {
        var movie = await dataAccessService.Movies
            .Where(x => x.Id == input.Id)
            .SingleAsync(cancellationToken);

        return movie.Adapt<GetMovieOutput>();
    }
}
