using Extensions.Web.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Application.Common.ValidationResults;

namespace ProductManagement.Application.Common.Behavior;

public class LoggingPipelineBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
where TRequest: TResponse
where TResponse : Result
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Starting request {@RequestName}, {@DateTimeUtc}",
            nameof(TRequest),
            DateTime.UtcNow);
        
        var result = await next();

        if (result.IsFailure)
        {
            _logger.LogError("Completed request {@RequestName}, {@Error}, {@DateTimeUtc}",
                nameof(TRequest),
                result.Error,
                DateTime.UtcNow);
        }

        _logger.LogDebug("Completed request {@RequestName}, {@DateTimeUtc}",
            nameof(TRequest),
            DateTime.UtcNow);
        
        return result;
    }
}