using Application.Tracks;
using Data;
using FluentValidation;
using MediatR;

namespace Application.Validations.Tracks
{
    public class CreateCommandValidator : AbstractValidator<Create.Command>
    {
        public CreateCommandValidator(DataContext ctx, IMediator mediator) 
        {
            RuleFor(x => x.Track.Title)
                .NotEmpty();
            
            RuleFor(x => x.Track.Author)
                .NotEmpty();
            
            RuleFor(x => x.Track.Audio)
                .NotEmpty();
        }
    }
}