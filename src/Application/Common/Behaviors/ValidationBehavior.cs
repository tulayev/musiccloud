using Application.Helpers;
using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, 
            CancellationToken cancellationToken, 
            RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var validationContext = new ValidationContext<TRequest>(request);

            var errors = (await Task.WhenAll(_validators
                .Select(async x => await x.ValidateAsync(validationContext))))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .Select(x => x.CustomState)
                .Cast<TResponse>();

            if (errors.Any())
            {
                return errors.First();
            }

            try
            {
                return await next();
            }
            catch(Exception ex)
            {
                return ApiResponse<TResponse>.Failure(ex).Value;
            }
        }
    }
}