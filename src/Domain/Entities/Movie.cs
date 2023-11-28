namespace Domain.Entities;

public record Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public float Rating { get; set; }
    public string Synopsis { get; set; } = "";
    public decimal TicketPrice { get; set; }
}
