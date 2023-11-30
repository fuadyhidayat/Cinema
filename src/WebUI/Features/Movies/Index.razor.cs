using Logic.Movies.GetMovies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace WebUI.Features.Movies;

public partial class Index
{
    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }

    private List<BreadcrumbItem> _breadcrumbItems = [];
    private bool _isLoading;
    private List<MovieDto> _movies = [];
    private string? _searchKeyword;
    private bool _bolehAddMovie = false;

    protected override async Task OnInitializedAsync()
    {
        if (authenticationState is not null)
        {
            var authState = await authenticationState;
            var user = authState?.User;

            if (user is not null)
            {
                _bolehAddMovie = user.IsInRole("Role XYZ");
            }
        }

        LoadBreadcrumbs();
        await LoadMovies();
    }

    private async Task LoadMovies()
    {
        _isLoading = true;

        var input = new GetMoviesInput
        {
            MinimumRating = 0.0f
        };

        var output = await _sender.Send(input);
        _movies = (List<MovieDto>)output.Movies;
        _isLoading = false;
    }

    private void LoadBreadcrumbs()
    {
        _breadcrumbItems =
        [
            new("Home", ""),
            new("Movies", "", disabled: true)
        ];
    }

    private bool FilterMovies(MovieDto movie)
    {
        if (string.IsNullOrWhiteSpace(_searchKeyword))
        {
            return true;
        }

        if (movie.Title.Contains(_searchKeyword, StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        if (movie.ReleaseDate.ToString("dd MMM yyyy").Contains(_searchKeyword, StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        if (movie.Rating.ToString("N1").Contains(_searchKeyword, StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        if (movie.TicketPrice.ToString().Contains(_searchKeyword, StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        return false;
    }

    private async Task SelectMovie(int id)
    {
        await _storage.SetAsync("MovieId", id);

        _navigationManager.NavigateTo("/Movies/DetailsBaru");
    }
}
