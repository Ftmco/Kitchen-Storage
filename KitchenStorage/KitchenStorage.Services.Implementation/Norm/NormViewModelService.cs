namespace KitchenStorage.Services.Implementation
{
    public class NormViewModelService : INormViewModel
    {
        public NormViewModel CreateNormViewModel(Norm norm)
            => new(
                Id: norm.Id,
                Name: norm.Name,
                Value: norm.Value,
                FoodId: norm.FoodId,
                InventoryId: norm.InventoryId,
                Status: norm.Status,
                CreateDate: norm.CreateDate.ToShamsi());

        public IEnumerable<NormViewModel> CreateNormViewModel(IEnumerable<Norm> norms)
            => norms.Select(CreateNormViewModel);
    }
}
