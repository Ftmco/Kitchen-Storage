namespace KitchenStorage.Services.Abstraction
{
    public interface IFoodViewModel
    {
        FoodViewModel CreateFoodViewModel(Food food);

        IEnumerable<FoodViewModel> CreateFoodViewModel(IEnumerable<Food> foods);
    }
}
