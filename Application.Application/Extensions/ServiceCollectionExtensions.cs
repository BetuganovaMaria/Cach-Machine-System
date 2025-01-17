using Application.Application.Admins;
using Application.Application.Users;
using Application.Contracts.Admins;
using Application.Contracts.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IAdminService, AdminService>();

        collection.AddScoped<CurrentUserManager>();
        collection.AddScoped<ICurrentUserService>(
            p => p.GetRequiredService<CurrentUserManager>());

        collection.AddScoped<CurrentAdminManager>();
        collection.AddScoped<ICurrentAdminService>(
            p => p.GetRequiredService<CurrentAdminManager>());

        return collection;
    }
}