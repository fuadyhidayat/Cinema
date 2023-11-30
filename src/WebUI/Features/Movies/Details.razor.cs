using Logic.Common.Extensions;
using Logic.Movies.Constants;
using Logic.Movies.GetMovie;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using WebUI.Features.Movies.Components;

namespace WebUI.Features.Movies;

public partial class Details
{
    [CascadingParameter]
    private Task<AuthenticationState>? authenticationState { get; set; }

    [Parameter]
    public int Id { get; set; }

    private List<BreadcrumbItem> _breadcrumbItems = [];
    private GetMovieOutput _movie = default!;
    private bool _isLoading;
    private Exception? _exception = null;

    private bool _bolehUbahMovie = false;
    private bool _bolehHapusMovie = false;

    protected override async Task OnInitializedAsync()
    {
        if (authenticationState is not null)
        {
            var authState = await authenticationState;
            var user = authState?.User;

            if (user is not null)
            {
                _bolehUbahMovie = user.HasPermission(PermissionFor.EditMovie);
                _bolehHapusMovie = user.HasPermission(PermissionFor.RemoveMovie);
            }
        }

        await LoadMovie();
        LoadBreadcrumbs();
    }

    private async Task LoadMovie()
    {
        try
        {
            _isLoading = true;

            var input = new GetMovieInput
            {
                Id = Id
            };

            _movie = await _sender.Send(input);
        }
        catch (Exception ex)
        {
            _exception = ex;
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void LoadBreadcrumbs()
    {
        if (_movie is null)
        {
            return;
        }

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
