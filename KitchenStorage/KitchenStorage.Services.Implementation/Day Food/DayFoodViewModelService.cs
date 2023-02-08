namespace KitchenStorage.Services.Implementation;

internal class DayFoodViewModelService : IDayFoodViewModel
{
    private readonly IBaseQuery<Food> _foodQuery;

    private readonly IBaseQuery<Day> _dayQuery;

    private readonly IFoodViewModel _foodViewModel;

    private readonly IDayViewModel _dayViewModel;

    public DayFoodViewModelService(IBaseQuery<Food> foodQuery, IBaseQuery<Day> dayQuery,
        IFoodViewModel foodViewModel, IDayViewModel dayViewModel)
    {
        _foodQuery = foodQuery;
        _dayQuery = dayQuery;
        _foodViewModel = foodViewModel;
        _dayViewModel = dayViewModel;
    }

    public async Task<DayFoodViewModel> CreateDayFoodViewModelAsync(DayFood dayFood)
    {
        Food? food = await _foodQuery.GetAsync(dayFood.FoodId);
        Day? day = await _dayQuery.GetAsync(dayFood.DayId);

        return new DayFoodViewModel(
            Id: dayFood.Id,
            Meal: dayFood.Meal,
            Food: food is null ? null : _foodViewModel.CreateFoodViewModel(food),
            Day: day is null ? null : _dayViewModel.CreateDayViewModel(day));
    }

    public async Task<IEnumerable<DayFoodViewModel>> CreateDayFoodViewModelAsync(IEnumerable<DayFood> daysFoods)
    {
        List<DayFoodViewModel> viewModels = new();

        foreach (var item in daysFoods)
            viewModels.Add(await CreateDayFoodViewModelAsync(item));

        return viewModels;
    }
}
