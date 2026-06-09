using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Application.Interfaces.IUseCases;
using WebApplication1.Application.UseCases;

namespace WebApplication1.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeUseCase, EmployeeUseCase>();
        services.AddScoped<IDepartmentUseCase, DepartmentUseCase>();
        services.AddScoped<IPositionUseCase, PositionUseCase>();

        return services;
    }
}
