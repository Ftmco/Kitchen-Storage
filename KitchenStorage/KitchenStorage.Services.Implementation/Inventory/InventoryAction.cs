namespace KitchenStorage.Services.Implementation;

public class InventoryAction : IInventoryAction
{
    private readonly IBaseCud<Inventory> _inventoryAction;

    private readonly IBaseQuery<Inventory> _inventoryQuery;

    private readonly IBaseCud<InventoryHistory> _inventoryHistoryCud;

    private readonly IBaseCud<InventoryHistoryList> _historyListCud;

    public InventoryAction
        (IBaseQuery<Inventory> inventoryQuery,
        IBaseCud<Inventory> inventoryAction,
        IBaseCud<InventoryHistory> inventoryHistoryCud,
        IBaseCud<InventoryHistoryList> historyListCud)
    {
        _inventoryQuery = inventoryQuery;
        _inventoryAction = inventoryAction;
        _inventoryHistoryCud = inventoryHistoryCud;
        _historyListCud = historyListCud;
    }

    public async Task<Either<InventoryActionStatus, Inventory>> CreateAsync(UpsertInventoryViewModel upsert)
    {
        Inventory newInventory = new()
        {
            Name = upsert.Name,
            Value = upsert.Value,
            TypeId = upsert.TypeId,
            AlertLimit = upsert.AlertLimit,
            Description = upsert.Description,
            GroupId = upsert.GroupId,
            Status = (byte)(upsert.Value > upsert.AlertLimit ? InventoryStatus.Available : InventoryStatus.AlertLimit),
        };

        if (await _inventoryAction.InsertAsync(newInventory))
        {
            await LogHistory(newInventory, upsert.Value, upsert.Value);
            return newInventory;
        }
        return InventoryActionStatus.Failed;
    }

    public async Task<Either<InventoryActionStatus, Inventory>> UpdateAsync(UpsertInventoryViewModel upsert)
    {
        var inventory = await _inventoryQuery.GetAsync(upsert.Id);
        if (inventory is null)
            return InventoryActionStatus.NotFound;

        inventory.Name = upsert.Name;
        inventory.TypeId = upsert.TypeId;
        inventory.Description = upsert.Description;
        inventory.AlertLimit = upsert.AlertLimit;
        inventory.GroupId = upsert.GroupId;

        if (upsert.Value != inventory.Value)
            await LogHistory(inventory, upsert.Value - inventory.Value, upsert.Value);

        inventory.Value = upsert.Value;

        inventory.Status = (byte)(inventory.Value > inventory.AlertLimit ? InventoryStatus.Available : InventoryStatus.AlertLimit);


        return await _inventoryAction.UpdateAsync(inventory) ?
        inventory : InventoryActionStatus.Failed;
    }

    public async Task<Either<InventoryActionStatus, Inventory>> UpsertAsync(UpsertInventoryViewModel upsert)
            => upsert.Id is null
            ? await CreateAsync(upsert)
            : await UpdateAsync(upsert);

    public async Task<InventoryActionStatus> DeleteAsync(Guid id)
            => await _inventoryAction.DeleteAsync(id)
                        ? InventoryActionStatus.Success
                        : InventoryActionStatus.Failed;

    async Task LogHistory(Inventory inventory, double value, double nextValue)
    {
        InventoryHistory history = new()
        {
            GenerateId = Guid.NewGuid(),
            Description = inventory.Description,
            Type = inventory.Value > nextValue ? (byte)InventoryHistoryType.Output : (byte)InventoryHistoryType.Input,
        };

        if (await _inventoryHistoryCud.InsertAsync(history))
            await _historyListCud.InsertAsync(new InventoryHistoryList()
            {
                HistoryId = history.Id,
                InventoryId = inventory.Id,
                Value = value,
                CurrentValue = inventory.Value,
                NextValue = nextValue
            });
    }
}
