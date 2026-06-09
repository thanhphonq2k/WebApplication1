using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Application.Interfaces.ICommon;
using WebApplication1.Application.Interfaces.IRepositories;
using WebApplication1.Application.Interfaces.IServices;
using WebApplication1.Infrastructure.Persistence;
using WebApplication1.Infrastructure.Repositories;
using WebApplication1.Infrastructure.Services;

namespace WebApplication1.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EmployeeAppCon")
            ?? throw new InvalidOperationException("Connection string 'EmployeeAppCon' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IFileStorageService, LocalFileStorageService>();

        return services;
    }
}
