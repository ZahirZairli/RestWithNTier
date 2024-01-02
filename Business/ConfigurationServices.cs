using Business.Services.Abstracts;
using Business.Services.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Business;

public static class ConfigurationServices
{
    public static IServiceCollection AddBusinessConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
}
