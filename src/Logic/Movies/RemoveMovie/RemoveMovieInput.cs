namespace Logic.Movies.RemoveMovie;

public record RemoveMovieInput : IRequest<RemoveMovieOutput>
{
    public required int Id { get; set; }
}
