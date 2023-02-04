using LanguageExt;
using Microsoft.EntityFrameworkCore;

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
                Description = upsert.Description,
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

            return await _inventoryAction.UpdateAsync(inventory) ?
            inventory : InventoryActionStatus.Failed;
        }

        public async Task<Either<InventoryActionStatus, Inventory>> UpsertAsync(UpsertInventoryViewModel upsert)
                => upsert.Id is null
                ? await CreateAsync(upsert)
                : await UpdateAsync(upsert);

        public async Task<Either<InventoryActionStatus, Inventory>> DeleteAsync(Guid id)
        {
            var inventory = await _inventoryQuery.GetAsync(id);

            if (inventory is null)
                return InventoryActionStatus.NotFound;

            inventory.Status = (byte)EntityState.Deleted;
            return await _inventoryAction.UpdateAsync(inventory) ?
                        inventory : InventoryActionStatus.Failed;
        }
    }
}
