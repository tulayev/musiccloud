using Domain;
using FluentValidation;

namespace Application.Tracks
{
    public class TrackValidator : AbstractValidator<Track>
    {
        public TrackValidator()
        {
            RuleFor(t => t.Title).NotEmpty();
            RuleFor(t => t.Author).NotEmpty();
            RuleFor(t => t.Genre).NotEmpty();
        }
    }
}