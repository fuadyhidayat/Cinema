using Logic.Movies.GetMovie;
using Logic.Movies.UpdateMovie;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace WebUI.Features.Movies;

public partial class Edit
{
    [Parameter]
    public int Id { get; set; }

    private List<BreadcrumbItem> _breadcrumbItems = [];
    private bool _isLoading;

    private EditModel _model = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadMovie();
        LoadBreadcrumbs();
    }

    private async Task LoadMovie()
    {
        _isLoading = true;

        var input = new GetMovieInput
        {
            Id = Id
        };

        var movie = await _sender.Send(input);

        _model = new()
        {
            Id = movie.Id,
            Title = movie.Title,
            Synopsis = movie.Synopsis,
            ReleaseDate = movie.ReleaseDate,
            Rating = movie.Rating,
            TicketPrice = movie.TicketPrice
        };

        _isLoading = false;
    }

    private void LoadBreadcrumbs()
    {
        _breadcrumbItems =
        [
            new("Home", ""),
            new("Movies", "Movies"),
            new(_model.Title, $"Movies/Details/{_model.Id}"),
            new("Edit", "", disabled: true)
        ];
    }

    public async Task Submit()
    {
        var input = new UpdateMovieInput()
        {
            Id = _model.Id,
            Title = _model.Title,
            Synopsis = _model.Synopsis,
            ReleaseDate = _model.ReleaseDate!.Value,
            Rating = _model.Rating,
            TicketPrice = _model.TicketPrice
        };

        await _sender.Send(input);

        _ = _snackbar.Add($"Movie {input.Title} has been successfully updated.", Severity.Success);

        _navigationManager.NavigateTo($"Movies/Details/{input.Id}");
    }
}

public class EditModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime? ReleaseDate { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public float Rating { get; set; }
    public decimal TicketPrice { get; set; }
}
