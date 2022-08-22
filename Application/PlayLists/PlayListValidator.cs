using Domain;
using FluentValidation;

namespace Application.PlayLists
{
    public class PlayListValidator : AbstractValidator<PlayList>
    {
        public PlayListValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}