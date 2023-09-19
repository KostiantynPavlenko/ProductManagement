using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Common.Behavior;

namespace ProductManagement.Application;

public static class Dependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Dependencies).Assembly));
        services.AddValidatorsFromAssembly(typeof(Dependencies).Assembly);
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        
        return services;
    }
}