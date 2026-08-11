using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace EduBridge.Application;
using EduBridge.Application.Common.Behaviors;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly);
        });

        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly);
            
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));    

        return services;
    }
}