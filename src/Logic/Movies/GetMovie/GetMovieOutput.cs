namespace Logic.Movies.GetMovie;

public record GetMovieOutput
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Synopsis { get; set; }
    public required DateTime ReleaseDate { get; set; }
    public required float Rating { get; set; }
    public required decimal TicketPrice { get; set; }
}
