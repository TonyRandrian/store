using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Store.Application.Commons
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;


        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next(cancellationToken);
            }

            ValidationContext<TRequest> context = new(request);

            var validationResults = new List<ValidationResult>();
            foreach (var validator in _validators)
            {
                var result = await validator.ValidateAsync(context, cancellationToken);
                validationResults.Add(result);
            }

            List<ValidationFailure> failures = [.. validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)];

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next(cancellationToken);
        }
    }
}
