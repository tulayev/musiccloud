using Application.Core;
using FluentValidation;
using MediatR;

namespace Application.Validations
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
                return await next();

            var validationContext = new ValidationContext<TRequest>(request);

            var errors = (await Task.WhenAll(_validators
                .Select(async x => await x.ValidateAsync(validationContext))))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            if (errors.Any())
                throw new ValidationException(errors);

            try
            {
                return await next();
            }
            catch(Exception e)
            {
                return Result<TResponse>.Failure(e).Value;
            }
        }
    }
}