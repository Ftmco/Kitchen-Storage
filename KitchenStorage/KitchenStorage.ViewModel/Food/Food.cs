namespace KitchenStorage.ViewModel
{

    public record FoodViewModel
        (Guid? Id, string Name, byte type, byte Status, string CreateDate);

    public record UpsertFoodViewModel
         (Guid? Id, string Name, byte type);

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
