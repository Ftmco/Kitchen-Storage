using LanguageExt;

namespace KitchenStorage.Services.Implementation
{
    public class NormAction : INormAction
    {
        private readonly IBaseCud<Norm> _action;

        public NormAction(IBaseCud<Norm> action)
        {
            _action = action;
        }

        public async Task<Either<FoodActionStatus, Norm>> CreateAsync(AddNormViewModel norm)
        {
            Norm newNorm = new()
            {
                Value = norm.Value,
                InventoryId = norm.InventoryId,
                FoodId = norm.FoodId,
            };

            return await _action.InsertAsync(newNorm) ?
                        newNorm : FoodActionStatus.Failed;
        }

        public async Task<FoodActionStatus> DeleteAsync(Guid id)
            => await _action.DeleteAsync(id)
                            ? FoodActionStatus.Success
                            : FoodActionStatus.Failed;
    }
}
