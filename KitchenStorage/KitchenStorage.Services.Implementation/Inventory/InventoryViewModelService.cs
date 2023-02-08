namespace KitchenStorage.Services.Implementation;

public class InventoryViewModelService : IInventoryViewModel
{
    private readonly IBaseQuery<MeasurementType> _typeQuery;

    private readonly IBaseQuery<Group> _groupQuery;

    private readonly IMeasurementTypeViewModel _typeViewModel;

    private readonly IGroupViewModel _groupViewModel;

    public InventoryViewModelService(IBaseQuery<MeasurementType> typeQuery, IMeasurementTypeViewModel typeViewModel, IBaseQuery<Group> groupQuery, IGroupViewModel groupViewModel)
    {
        _typeQuery = typeQuery;
        _typeViewModel = typeViewModel;
        _groupQuery = groupQuery;
        _groupViewModel = groupViewModel;
    }

    public async Task<InventoryViewModel> CreateInventoryViewModelAsync(Inventory inventory)
    {
        MeasurementType? type = await _typeQuery.GetAsync(inventory.TypeId);
        Group? group = await _groupQuery.GetAsync(inventory.GroupId);

        return new(Id: inventory.Id,
             Name: inventory.Name,
             Value: inventory.Value,
             AlertLimit: inventory.AlertLimit,
             Description: inventory.Description,
             CreateDate: inventory.CreateDate.ToShamsi(),
             Status: inventory.Status,
             Type: type is null ? null : _typeViewModel.CreateMeasurementTypeViewModel(type),
             Group: group is null ? null : _groupViewModel.CreateGroupViewModel(group));
    }

    public async Task<IEnumerable<InventoryViewModel>> CreateInventoryViewModelAsync(IEnumerable<Inventory> inventories)
    {
        List<InventoryViewModel> viewModels = new();

        foreach (var item in inventories)
            viewModels.Add(await CreateInventoryViewModelAsync(item));

        return viewModels;
    }

    public InventoryPreviewViewModel CreatePreviewInventiryViewModel(Inventory inventory)
        => new(Id: inventory.Id,
            Name: inventory.Name);

    public IEnumerable<InventoryPreviewViewModel> CreatePreviewInventiryViewModel(IEnumerable<Inventory> inventory)
        => inventory.Select(CreatePreviewInventiryViewModel);
}
