using Logic.Movies.GetMovie;
using MudBlazor;

namespace WebUI.Features.Movies;

public partial class DetailsBaru
{
    private List<BreadcrumbItem> _breadcrumbItems = [];
    private GetMovieOutput _movie = default!;
    private bool _isLoading;

    protected override async Task OnInitializedAsync()
    {
        await LoadMovie();
        LoadBreadcrumbs();
    }

    private async Task LoadMovie()
    {
        var movieId = await _storage.GetAsync<int>("MovieId");

        if (!movieId.Success)
        {
            _snackbar.Add("Movie ID belum ente pilih!", Severity.Error);
            _navigationManager.NavigateTo("/Movies");

            return;
        }

        _isLoading = true;

        var input = new GetMovieInput
        {
            Id = movieId.Value
        };

        _movie = await _sender.Send(input);

        _isLoading = false;
    }

    private void LoadBreadcrumbs()
    {
        _breadcrumbItems =
        [
            new("Home", ""),
            new("Movies", "Movies"),
            new(_movie.Title, "", disabled: true)
        ];
    }
}
