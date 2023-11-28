namespace Logic.Movies.AddMovie;

public record AddMovieInput : IRequest<AddMovieOutput>
{
    public required string Title { get; init; }
    public required DateTime ReleaseDate { get; init; }
    public required string Synopsis { get; init; }
    public required float Rating { get; init; }
    public required decimal TicketPrice { get; init; }
}
