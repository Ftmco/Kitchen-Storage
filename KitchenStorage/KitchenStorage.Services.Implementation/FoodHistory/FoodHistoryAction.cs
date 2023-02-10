using KitchenStorage.Services.Abstraction;

namespace KitchenStorage.Services.Implementation
{
    public class FoodHistoryAction : IFoodHistoryAction
    {
        private readonly IBaseCud<FoodHistory> _action;
        private readonly IBaseQuery<FoodHistory> _query;

        public FoodHistoryAction(IBaseCud<FoodHistory> action, IBaseQuery<FoodHistory> query)
        {
            _action = action;
            _query = query;
        }

        public async Task<Either<FoodHistoryActionStatus, FoodHistory>> CreateAsync(UpsertFoodHistoryViewModel upsert)
        {
            FoodHistory newhistory = new()
            {
                DayId = upsert.DayId,
                FoodId = upsert.FoodId,
                Count = upsert.Count,
                Meal = upsert.Meal,
            };

            return await _action.InsertAsync(newhistory) ?
                        newhistory : FoodHistoryActionStatus.Failed;
        }

        public async Task<FoodHistoryActionStatus> DeleteAsync(Guid id)
               => await _action.DeleteAsync(id)
                            ? FoodHistoryActionStatus.Success
                            : FoodHistoryActionStatus.Failed;

        public async Task<Either<FoodHistoryActionStatus, FoodHistory>> UpdateAsync(UpsertFoodHistoryViewModel upsert)
        {
            var history = await _query.GetAsync(upsert.Id);
            if (history is null)
                return FoodHistoryActionStatus.NotFound;

            history.DayId = upsert.DayId;
            history.FoodId = upsert.FoodId;
            history.Count = upsert.Count;
            history.Description = upsert.Description;

            return await _action.UpdateAsync(history) ?
            history : FoodHistoryActionStatus.Failed;
        }

        public async Task<Either<FoodHistoryActionStatus, FoodHistory>> UpsertAsync(UpsertFoodHistoryViewModel upsert)
            => upsert.Id is null ? await CreateAsync(upsert) : await UpdateAsync(upsert);
    }
}
