namespace KitchenStorage.Services.Implementation;

public class NormViewModelService : INormViewModel
{
    private readonly IBaseQuery<Inventory> _inventoryQuery;

    private readonly IBaseQuery<MeasurementType> _typeQuery;

    private readonly IInventoryViewModel _inventoryViewModel;

    private readonly IMeasurementTypeViewModel _typeViewModel;

    public NormViewModelService(IBaseQuery<Inventory> inventoryQuery, IInventoryViewModel inventoryViewModel,
        IBaseQuery<MeasurementType> typeQuery, IMeasurementTypeViewModel typeViewModel)
    {
        _inventoryQuery = inventoryQuery;
        _inventoryViewModel = inventoryViewModel;
        _typeQuery = typeQuery;
        _typeViewModel = typeViewModel;
    }

    public async Task<NormViewModel> CreateNormViewModelAsync(Norm norm)
    {
        Inventory? inventory = await _inventoryQuery.GetAsync(norm.InventoryId);
        MeasurementType? type = await _typeQuery.GetAsync(norm.TypeId);

        return new(
            Id: norm.Id,
            Value: norm.Value,
            FoodId: norm.FoodId,
            Status: norm.Status,
            CreateDate: norm.CreateDate.ToShamsi(),
            Type: type is null ? null : _typeViewModel.CreateMeasurementTypeViewModel(type),
            Inventory: inventory is null ? null : _inventoryViewModel.CreatePreviewInventiryViewModel(inventory));

    }

    public async Task<IEnumerable<NormViewModel>> CreateNormViewModelAsync(IEnumerable<Norm> norms)
    {
        List<NormViewModel> viewModels = new();

        foreach (var item in norms)
            viewModels.Add(await CreateNormViewModelAsync(item));

        return viewModels;
    }
}
