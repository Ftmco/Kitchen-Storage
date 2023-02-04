namespace KitchenStorage.Services.Implementation
{
    public class FoodViewModelService : IFoodViewModel
    {
        public FoodViewModel CreateFoodViewModel(Food food)
            => new(Id: food.Id,
                Name: food.Name,
                type: food.Type,
                CreateDate: food.CreateDate.ToShamsi(),
                Status: food.Status);

        public IEnumerable<FoodViewModel> CreateFoodViewModel(IEnumerable<Food> foods)
            => foods.Select(CreateFoodViewModel);
    }
}
