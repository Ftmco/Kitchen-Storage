namespace KitchenStorage.Services.Implementation;

public class HistoryViewModelService : IHistoryViewModel
{
    private readonly IBaseQuery<Food> _foodQuery;
    private readonly IBaseQuery<Day> _dayQuery;
    private readonly IBaseQuery<Inventory> _inventoryQuery;
    private readonly IBaseQuery<InventoryHistoryList> _inventoryHistoryListQuery;
    private readonly IFoodViewModel _foodViewModel;
    private readonly IDayViewModel _dayViewModel;
    private readonly IInventoryViewModel _inventoryViewModel;

    public HistoryViewModelService
        (IBaseQuery<Food> foodQuery,
        IBaseQuery<Day> dayQuery,
        IFoodViewModel foodViewModel,
        IDayViewModel dayViewModel,
        IBaseQuery<Inventory> inventoryQuery,
        IBaseQuery<InventoryHistoryList> inventoryHistoryListQuery,
        IInventoryViewModel inventoryViewModel)
    {
        _foodQuery = foodQuery;
        _dayQuery = dayQuery;
        _foodViewModel = foodViewModel;
        _dayViewModel = dayViewModel;
        _inventoryQuery = inventoryQuery;
        _inventoryHistoryListQuery = inventoryHistoryListQuery;
        _inventoryViewModel = inventoryViewModel;
    }

    public async Task<FoodHistoryViewModel> CreateFoodHistoryViewModel(FoodHistory foodHistory)
    {
        Day? day = await _dayQuery.GetAsync(foodHistory.DayId);
        Food? food = await _foodQuery.GetAsync(foodHistory.FoodId);

        return new(Id: foodHistory.Id,
         Day: day is null ? null : _dayViewModel.CreateDayViewModel(day),
         Food: food is null ? null : _foodViewModel.CreateFoodViewModel(food),
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

    public async Task<InventoryHistoryViewModel> CreateInventoryHisstoryViewModelAsync(InventoryHistory history)
    {
        IEnumerable<InventoryHistoryList> historyList = await _inventoryHistoryListQuery.GetAllAsync(hl => hl.HistoryId == history.Id);

        return new(Id: history.Id,
            Description: history.Description,
            CreateDate: history.CreateDate.ToShamsi(),
            Type: history.Type,
            HistoryList: await CreateInventoryHistoryItemViewModel(historyList));
    }

    public async Task<IEnumerable<InventoryHistoryViewModel>> CreateInventoryHisstoryViewModelAsync(IEnumerable<InventoryHistory> history)
    {
        List<InventoryHistoryViewModel> viewModels = new();

        foreach (var item in history)
            viewModels.Add(await CreateInventoryHisstoryViewModelAsync(item));

        return viewModels;
    }

    public async Task<IEnumerable<InventoryHistoryListViewModel>> CreateInventoryHistoryItemViewModel(IEnumerable<InventoryHistoryList> inventoryHistories)
    {
        List<InventoryHistoryListViewModel> viewModels = new();

        foreach (var item in inventoryHistories)
            viewModels.Add(await CreateInventoryHistoryItemViewModel(item));

        return viewModels;
    }

    public async Task<InventoryHistoryListViewModel> CreateInventoryHistoryItemViewModel(InventoryHistoryList inventoryHistory)
    {
        Inventory? inventory = await _inventoryQuery.GetAsync(inventoryHistory.InventoryId);

        return new(Id: inventoryHistory.Id,
            HistoryId: inventoryHistory.HistoryId,
            Value: inventoryHistory.Value,
            CurrentValue: inventoryHistory.CurrentValue,
            NextValue: inventoryHistory.NextValue,
            Inventory: inventory is null ? null : _inventoryViewModel.CreatePreviewInventiryViewModel(inventory));
    }
}

