using Infrastructure.Persistence.Context;
using Infrastructure.Security;
using Domain.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Application.UseCases.Clients.Create;
using Application.UseCases.Clients.Delete;
using Domain.Security;
using Application.UseCases.Clients.List;
using Application.Repositories;
using Application.UseCases.Payment.Create;

namespace Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")
                )
        );

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IWarmupService, SystemRepository>();
        services.AddScoped<ListClientsUseCase>();
        services.AddScoped<CreateClientUseCase>();
        services.AddScoped<DeleteClientUseCase>();
        services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<CreatePaymentUseCase>();

        return services;
    }
}