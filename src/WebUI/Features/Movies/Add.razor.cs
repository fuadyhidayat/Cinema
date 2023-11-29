using FluentValidation;
using Logic.Movies.AddMovie;
using Logic.Movies.Constants;
using MudBlazor;
using Severity = MudBlazor.Severity;

namespace WebUI.Features.Movies;

public partial class Add
{
    private List<BreadcrumbItem> _breadcrumbItems = [];
    private bool _isLoading;
    private Exception? _exception = null;
    private MudForm _form = default!;
    AddModelValidator _validator = new();

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
        await _form.Validate();

        if (!_form.IsValid)
        {
            foreach (var erroMessage in _form.Errors)
            {
                _ = _snackbar.Add(erroMessage, Severity.Info);
            }

            return;
        }

        try
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

            _ = _snackbar.Add($"Movie {_model.Title} has been successfully added.", Severity.Success);
            _navigationManager.NavigateTo($"Movies/Details/{output.Id}");
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
}

public class AddModel
{
    public string Title { get; set; } = string.Empty;
    public DateTime? ReleaseDate { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public float Rating { get; set; }
    public decimal TicketPrice { get; set; }
}

public class AddModelValidator : AbstractValidator<AddModel>
{
    public AddModelValidator()
    {
        _ = RuleFor(x => x.Title)
            .MinimumLength(MinimumLengthFor.Title)
            .MaximumLength(MaximumLengthFor.Title);

        _ = RuleFor(x => x.ReleaseDate)
            .NotNull()
            .NotEmpty()
            .LessThan(DateTime.Now.Date);

        _ = RuleFor(x => x.Synopsis)
            .MinimumLength(MinimumLengthFor.Synopsis)
            .MaximumLength(MaximumLengthFor.Synopsis);

        _ = RuleFor(x => x.Rating)
            .InclusiveBetween(MinimumValueFor.Rating, MaximumValueFor.Rating);

        _ = RuleFor(x => x.TicketPrice)
            .InclusiveBetween(MinimumValueFor.TicketPrice, MaximumValueFor.TicketPrice);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AddModel>.CreateWithOptions((AddModel)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
            return Array.Empty<string>();

        return result.Errors.Select(e => e.ErrorMessage);
    };
}
