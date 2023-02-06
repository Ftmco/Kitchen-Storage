namespace KitchenStorage.Services.Abstraction
{
    public interface IInventoryAction
    {
        Task<Either<InventoryActionStatus, Inventory>> UpsertAsync(UpsertInventoryViewModel upsert);

        Task<Either<InventoryActionStatus, Inventory>> CreateAsync(UpsertInventoryViewModel upsert);

        Task<Either<InventoryActionStatus, Inventory>> UpdateAsync(UpsertInventoryViewModel upsert);

        Task<InventoryActionStatus> DeleteAsync(Guid id);
    }
}
