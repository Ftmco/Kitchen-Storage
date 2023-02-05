﻿namespace KitchenStorage.Services.Abstraction
{
    public interface IGetInventory
    {
        Task<IEnumerable<Inventory>> InventorysAsync();
        Task<PaginationResult<IEnumerable<Inventory>>> InventorysAsync(int page, int count);
    }
}
