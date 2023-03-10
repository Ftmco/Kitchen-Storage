namespace KitchenStorage.Services.Implementation
{
    public class FoodAction : IFoodAction
    {
        private readonly IBaseCud<Food> _foodAction;
        private readonly IBaseQuery<Food> _foodQuery;

        public FoodAction
            (IBaseCud<Food> foodAction,
            IBaseQuery<Food> foodQuery)
        {
            _foodAction = foodAction;
            _foodQuery = foodQuery;
        }

        public async Task<Either<FoodActionStatus, Food>> CreateAsync(UpsertFoodViewModel upsert)
        {
            Food newFood = new()
            {
                Name = upsert.Name,
                Type = upsert.Type,
            };

            return await _foodAction.InsertAsync(newFood) ?
                        newFood : FoodActionStatus.Failed;
        }

        public async Task<FoodActionStatus> DeleteAsync(Guid id)
                => await _foodAction.DeleteAsync(id)
                                ? FoodActionStatus.Success
                                : FoodActionStatus.Failed;

        public async Task<Either<FoodActionStatus, Food>> UpdateAsync(UpsertFoodViewModel upsert)
        {
            Food? food = await _foodQuery.GetAsync(upsert.Id);
            if (food is null)
                return FoodActionStatus.NotFound;

            food.Name = upsert.Name;
            food.Type = upsert.Type;
            return await _foodAction.UpdateAsync(food) ?
                        food : FoodActionStatus.Failed;
        }

        public async Task<Either<FoodActionStatus, Food>> UpsertAsync(UpsertFoodViewModel upsert)
            => upsert.Id is null
            ? await CreateAsync(upsert) : await UpdateAsync(upsert);
    }
}
