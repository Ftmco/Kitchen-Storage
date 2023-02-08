namespace KitchenStorage.Services.Implementation;

public class NormViewModelService : INormViewModel
{
    private readonly IBaseQuery<Inventory> _inventoryQuery;

    private readonly IInventoryViewModel _inventoryViewModel;

    public NormViewModelService(IBaseQuery<Inventory> inventoryQuery, IInventoryViewModel inventoryViewModel)
    {
        _inventoryQuery = inventoryQuery;
        _inventoryViewModel = inventoryViewModel;
    }

    public async Task<NormViewModel> CreateNormViewModelAsync(Norm norm)
    {
        Inventory? inventory = await _inventoryQuery.GetAsync(norm.InventoryId);

        return new(
            Id: norm.Id,
            Value: norm.Value,
            FoodId: norm.FoodId,
            Status: norm.Status,
            CreateDate: norm.CreateDate.ToShamsi(),
            Inventory: inventory is null ? null : new(Id: inventory.Id, Name: inventory.Name));

    }

    public async Task<IEnumerable<NormViewModel>> CreateNormViewModelAsync(IEnumerable<Norm> norms)
    {
        List<NormViewModel> viewModels = new();

        foreach (var item in norms)
            viewModels.Add(await CreateNormViewModelAsync(item));

        return viewModels;
    }
}
