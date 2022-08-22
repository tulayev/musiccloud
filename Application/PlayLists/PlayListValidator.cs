using FluentValidation;
using Models;

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