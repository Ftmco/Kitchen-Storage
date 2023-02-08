namespace KitchenStorage.Services.Abstraction;

public interface IDayFoodViewModel
{
    Task<DayFoodViewModel> CreateDayFoodViewModelAsync(DayFood dayFood);

    Task<IEnumerable<DayFoodViewModel>> CreateDayFoodViewModelAsync(IEnumerable<DayFood> daysFoods);
}
