using KitchenStorage.DataBase.Context;
using KitchenStorage.Services.Implementation.Base;
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
        services.AddScoped<IBaseCud<Group>, BaseCud<Group>>();
        services.AddScoped<IBaseCud<Inventory>, BaseCud<Inventory>>();
    }

    static void AddBaseQueryServices(this IServiceCollection services)
    {
        services.AddScoped<IBaseQuery<Group>, BaseQuery<Group>>();
        services.AddScoped<IBaseQuery<Inventory>, BaseQuery<Inventory>>();
    }

    static void AddActionServices(this IServiceCollection services)
    {
        #region Group

        services.AddTransient<IGroupAction, GroupAction>();
        services.AddTransient<IGroupGet, GroupGet>();
        services.AddTransient<IGroupViewModel, GroupViewModelService>();

        #endregion

        #region Inventory

        services.AddTransient<IInventoryAction, InventoryAction>();
        services.AddTransient<IGetInventory, GetInventory>();
        services.AddTransient<IInventoryViewModel, InventoryViewModelService>();

        #endregion
    }
}
