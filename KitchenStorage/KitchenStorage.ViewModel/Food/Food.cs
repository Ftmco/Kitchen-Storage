namespace KitchenStorage.ViewModel
{

    public record FoodViewModel
        (Guid? Id, string Name, byte Type, byte Status, string CreateDate);

    public record UpsertFoodViewModel
         (Guid? Id, string Name, byte Type);

    public enum FoodType
    {
        Soldier = 0,
        Employee = 1
    }

    public enum FoodActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2
    }

}
