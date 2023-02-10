namespace KitchenStorage.ViewModel
{
    public record FoodHistoryViewModel
    (Guid? Id, DayViewModel Day, FoodViewModel Food, int Count, string Meal, string Description, string CreateDate);

    public record UpsertFoodHistoryViewModel
         (Guid? Id, Guid DayId, Guid FoodId, int Count, string Meal, string Description);


    public enum FoodHistoryActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2
    }
}
