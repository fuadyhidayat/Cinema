﻿using Logic.Movies.Constants;

namespace Logic.Movies.UpdateMovie;

public record UpdateMovieInput : IRequest<UpdateMovieOutput>
{
    public required int Id { get; init; }
    public required string Title { get; init; }
    public required DateTime ReleaseDate { get; init; }
    public required string Synopsis { get; init; }
    public required float Rating { get; init; }
    public required decimal TicketPrice { get; init; }
}

public class UpdateMovieInputValidator : AbstractValidator<UpdateMovieInput>
{
    public UpdateMovieInputValidator()
    {
        _ = RuleFor(x => x.Id)
            .GreaterThan(0);

        _ = RuleFor(x => x.Title)
            .MinimumLength(MinimumLengthFor.Title)
            .MaximumLength(MaximumLengthFor.Title);

        _ = RuleFor(x => x.ReleaseDate)
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
}