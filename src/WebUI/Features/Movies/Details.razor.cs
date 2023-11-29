using Logic.Movies.GetMovie;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Features.Movies.Components;

namespace WebUI.Features.Movies;

public partial class Details
{
    [Parameter]
    public int Id { get; set; }

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
        _isLoading = true;

        var input = new GetMovieInput
        {
            Id = Id
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

    private async Task ShowDialogRemoveMovie()
    {
        var parameters = new DialogParameters
        {
            { nameof(DialogRemoveMovie.MovieId), _movie.Id },
            { nameof(DialogRemoveMovie.MovieTitle), _movie.Title }
        };

        var dialog = _dialogService.Show<DialogRemoveMovie>("Remove Movie", parameters);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            _navigationManager.NavigateTo("Movies");
        }
    }
}
