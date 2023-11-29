using Logic.Common.Exceptions;
using Logic.Movies.AddMovie;
using MudBlazor;

namespace WebUI.Features.Movies;

public partial class Add
{
    private List<BreadcrumbItem> _breadcrumbItems = [];
    private bool _isLoading;

    private readonly AddModel _model = new()
    {
        Title = string.Empty,
        Synopsis = string.Empty,
        ReleaseDate = DateTime.Now,
        Rating = 7.0f,
        TicketPrice = 50000m
    };

    protected override void OnInitialized()
    {
        LoadBreadcrumbs();
    }

    private void LoadBreadcrumbs()
    {
        _breadcrumbItems =
        [
            new("Home", ""),
            new("Movies", "Movies"),
            new("Add", "", disabled: true)
        ];
    }

    public async Task Submit()
    {
        _isLoading = true;

        var input = new AddMovieInput()
        {
            Title = _model.Title,
            Synopsis = _model.Synopsis,
            ReleaseDate = _model.ReleaseDate!.Value,
            Rating = _model.Rating,
            TicketPrice = _model.TicketPrice
        };

        var output = await _sender.Send(input);

        if (output is null)
        {
            _ = _snackbar.Add($"Failed to add movie {_model.Title}.", Severity.Error);

            return;
        }

        _isLoading = false;
        _ = _snackbar.Add($"Movie {_model.Title} has been successfully added.", Severity.Success);
        _navigationManager.NavigateTo($"Movies/Details/{output.Id}");
    }
}

public class AddModel
{
    public string Title { get; set; } = string.Empty;
    public DateTime? ReleaseDate { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public float Rating { get; set; }
    public decimal TicketPrice { get; set; }
}
