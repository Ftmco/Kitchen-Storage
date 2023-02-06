namespace KitchenStorage.Services.Implementation;

public class InventoryViewModelService : IInventoryViewModel
{
    private readonly IBaseQuery<MeasurementType> _typeQuery;

    private readonly IMeasurementTypeViewModel _typeViewModel;

    public InventoryViewModelService(IBaseQuery<MeasurementType> typeQuery, IMeasurementTypeViewModel typeViewModel)
    {
        _typeQuery = typeQuery;
        _typeViewModel = typeViewModel;
    }

    public async Task<InventoryViewModel> CreateInventoryViewModelAsync(Inventory inventory)
    {
        var type = await _typeQuery.GetAsync(inventory.TypeId);

        return new(Id: inventory.Id,
             Name: inventory.Name,
             Value: inventory.Value,
             AlertLimit: inventory.AlertLimit,
             Description: inventory.Description,
             CreateDate: inventory.CreateDate.ToShamsi(),
             Status: inventory.Status,
             Type: type is null ? null : _typeViewModel.CreateMeasurementTypeViewModel(type));
    }

    public async Task<IEnumerable<InventoryViewModel>> CreateInventoryViewModelAsync(IEnumerable<Inventory> inventories)
    {
        List<InventoryViewModel> viewModels = new();

        foreach (var item in inventories)
            viewModels.Add(await CreateInventoryViewModelAsync(item));

        return viewModels;
    }
}
