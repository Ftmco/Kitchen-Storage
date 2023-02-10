using System.ComponentModel;

namespace KitchenStorage.Services.Implementation
{
    public class FoodHistoryViewModelService : IFoodHistoryViewModel
    {
        private readonly IBaseQuery<Food> _foodQuery;
        private readonly IBaseQuery<Day> _dayQuery;
        private readonly IFoodViewModel foodViewModel;
        private readonly IDayViewModel dayViewModel;

        public FoodHistoryViewModelService
            (IBaseQuery<Food> foodQuery,
            IBaseQuery<Day> dayQuery,
            IFoodViewModel foodViewModel,
            IDayViewModel dayViewModel)
        {
            _foodQuery = foodQuery;
            _dayQuery = dayQuery;
            this.foodViewModel = foodViewModel;
            this.dayViewModel = dayViewModel;
        }

        public async Task<FoodHistoryViewModel> CreateFoodHistoryViewModel(FoodHistory foodHistory)
        {
            var day = await _dayQuery.GetAsync(foodHistory.DayId);
            var food = await _foodQuery.GetAsync(foodHistory.FoodId);
            return new(Id: foodHistory.Id,
             Day: day is null ? null : dayViewModel.CreateDayViewModel(day),
             Food: food is null ? null : foodViewModel.CreateFoodViewModel(food),
             Count: foodHistory.Count,
             Meal: foodHistory.Meal,
             Description: foodHistory.Description,
        CreateDate: foodHistory.CreateDate.ToShamsi());
        }


        public async Task<IEnumerable<FoodHistoryViewModel>> CreateFoodHistoryViewModel(IEnumerable<FoodHistory> foodHistories)
        {
            List<FoodHistoryViewModel> viewModels = new();

            foreach (var item in foodHistories)
                viewModels.Add(await CreateFoodHistoryViewModel(item));

            return viewModels;
        }
    }
}

