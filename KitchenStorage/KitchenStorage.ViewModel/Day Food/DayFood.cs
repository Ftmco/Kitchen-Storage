namespace KitchenStorage.ViewModel;

public record DayFoodViewModel(Guid Id, string Meal, FoodViewModel? Food, DayViewModel? Day);

public record UpsertDayFoodViewModel(Guid? Id, string Meal, Guid FoodId, Guid DayId);

public record MakeMealViewModel(Guid Id, int Count, string Description);

public enum DayFoodActionStatus
{
    Success = 0,
    Failed = 1,
    NotFound = 2,
    LackOfInventory = 3,
}