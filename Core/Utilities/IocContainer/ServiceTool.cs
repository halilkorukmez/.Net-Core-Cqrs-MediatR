using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IocContainer;

public class ServiceTool
{
    public static IServiceProvider ServiceProvider { get; set; }

    public static IServiceCollection Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }
}