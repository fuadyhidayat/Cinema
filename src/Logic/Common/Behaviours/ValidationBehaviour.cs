using Logic.Common.Exceptions;
using Microsoft.Extensions.Logging;

namespace Logic.Common.Behaviours;

public class ValidationBehaviour<TRequest, TResponse>(
    ILogger<TRequest> logger,
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validators.Any())
        {
            logger.LogInformation("Validating {RequestName}.", typeof(TRequest).Name);

            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));
            var errorMessages = validationResults
                .Where(x => x.Errors.Count is not 0)
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);

            if (errorMessages.Any())
            {
                var requestName = typeof(TRequest).Name;
                var exception = new AppValidationException(errorMessages);

                logger.LogError("Validation failed when processing {RequestName}.\n{Summary}",
                    requestName, exception);

                throw exception;
            }
        }

        return await next();
    }
}
