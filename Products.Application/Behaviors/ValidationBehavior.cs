using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            foreach (var validator in _validators)
            {
                var validationResult = await validator.ValidateAsync(
                    new ValidationContext<TRequest>(request),
                    cancellationToken);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(
                        validationResult.Errors);
                }
            }

            return await next();
        }
    }
}
