namespace Logic.Movies.GetMovies;

public record MovieDto
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required DateTime ReleaseDate { get; init; }
    public required float Rating { get; init; }
    public required decimal TicketPrice { get; init; }
}
