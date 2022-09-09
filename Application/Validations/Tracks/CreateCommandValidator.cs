using Application.Core;
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
                .NotEmpty()
                .WithState(x => Result<bool>.Failure(new Exception("Title is required")));
            
            RuleFor(x => x.Track.Author)
                .NotEmpty()
                .WithState(x => Result<bool>.Failure(new Exception("Author is required")));
        }
    }
}