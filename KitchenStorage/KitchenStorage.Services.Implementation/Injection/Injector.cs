using KitchenStorage.DataBase.Context;
using KitchenStorage.Services.Implementation.Base;
using KitchenStorage.Services.Implementation.MeasurmentType;
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

        services.AddBaseServices();
        services.AddActionServices();
    }

    static void AddBaseServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseCud<>), typeof(BaseCud<>));
        services.AddScoped(typeof(IBaseQuery<>), typeof(BaseQuery<>));
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

        #region MeasurmentType

        services.AddTransient<IMeasurementTypeAction, MeasurementTypeAction>();
        services.AddTransient<IGetMeasurementType, GetMeasurementType>();
        services.AddTransient<IMeasurementTypeViewModel, MeasurementTypeViewModelService>();
        #endregion

        #region Note

        services.AddTransient<INoteAction, NoteAction>();
        services.AddTransient<IGetNote, GetNote>();
        services.AddTransient<INoteViewModel, NoteViewModelService>();

        #endregion

        #region Food

        services.AddTransient<IFoodAction, FoodAction>();
        services.AddTransient<IGetFood, GetFood>();
        services.AddTransient<IFoodViewModel, FoodViewModelService>();

        #endregion

        #region Norm

        services.AddTransient<IGetNorm, GetNorm>();
        services.AddTransient<INormAction, NormAction>();
        services.AddTransient<INormViewModel, NormViewModelService>();

        #endregion

        #region Day

        services.AddTransient<IGetDay, GetDay>();
        services.AddTransient<IDayAction, DayAction>();
        services.AddTransient<IDayViewModel, DayViewModelService>();

        #endregion
    }
}
