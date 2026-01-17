using Application.CQRS.Tracks.Commands;
using Application.Helpers;
using Data;
using FluentValidation;
using MediatR;

namespace Application.Validators.Tracks
{
    public class CreateTrackValidator : AbstractValidator<CreateTrackCommand>
    {
        public CreateTrackValidator() 
        {
            RuleFor(x => x.CreateTrackDto.Title)
                .NotEmpty()
                .WithState(x => ApiResponse<bool>.Failure(new Exception("Title is required")));
            
            RuleFor(x => x.CreateTrackDto.Author)
                .NotEmpty()
                .WithState(x => ApiResponse<bool>.Failure(new Exception("Author is required")));
        }
    }
}
