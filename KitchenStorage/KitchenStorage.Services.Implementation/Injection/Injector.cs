using KitchenStorage.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenStorage.Services.Implementation.Injection;

public static class Injector
{
    public static void AddKitchenServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("KitchenDB") ?? "";
        KitchenContext.ConnectionString = connectionString;

        services.AddDbContext<KitchenContext>(options => options.UseSqlServer(connectionString));

        services.AddBaseCudServices();
        services.AddBaseQueryServices();
        services.AddActionServices();
    }

    static void AddBaseCudServices(this IServiceCollection services)
    {

    }

    static void AddBaseQueryServices(this IServiceCollection services)
    {

    }

    static void AddActionServices(this IServiceCollection services)
    {

    }
}
