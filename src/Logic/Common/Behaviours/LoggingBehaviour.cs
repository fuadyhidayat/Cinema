using Logic.Common.Extensions;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Logic.Common.Behaviours;

public class LoggingBehaviour<TRequest>(ILogger<TRequest> logger)
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var formattedRequest = request.ToPrettyJson();

        logger.LogInformation("Processing {RequestName}.\n{RequestName}\n{RequestObject}",
           requestName, requestName, formattedRequest);

        return Task.CompletedTask;
    }
}
