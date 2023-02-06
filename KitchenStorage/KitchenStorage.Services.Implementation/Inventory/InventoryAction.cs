using LanguageExt;

namespace KitchenStorage.Services.Implementation
{
    public class InventoryAction : IInventoryAction
    {
        private readonly IBaseCud<Inventory> _inventoryAction;
        private readonly IBaseQuery<Inventory> _inventoryQuery;

        public InventoryAction
            (IBaseQuery<Inventory> inventoryQuery,
            IBaseCud<Inventory> inventoryAction)
        {
            _inventoryQuery = inventoryQuery;
            _inventoryAction = inventoryAction;
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
            };

            return await _inventoryAction.InsertAsync(newInventory) ?
                        newInventory : InventoryActionStatus.Failed;
        }

        public async Task<Either<InventoryActionStatus, Inventory>> UpdateAsync(UpsertInventoryViewModel upsert)
        {
            var inventory = await _inventoryQuery.GetAsync(upsert.Id);
            if (inventory is null)
                return InventoryActionStatus.NotFound;

            inventory.Name = upsert.Name;
            inventory.Value = upsert.Value;
            inventory.TypeId = upsert.TypeId;
            inventory.Description = upsert.Description;
            inventory.AlertLimit = upsert.AlertLimit;
            inventory.GroupId = upsert.GroupId;

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
    }
}
