using Logic.Common.Extensions;
using Logic.Services.CurrentUser;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Logic.Common.Behaviours;

public class LoggingBehaviour<TRequest>(ICurrentUserService currentUserService, ILogger<TRequest> logger)
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var formattedRequest = request.ToPrettyJson();

        logger.LogInformation("Processing {RequestName} for {Username}.\n{RequestName}\n{RequestObject}",
           requestName, currentUserService.Username, requestName, formattedRequest);

        return Task.CompletedTask;
    }
}
