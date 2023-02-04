namespace KitchenStorage.Services.Abstraction
{
    public interface IGetMeasurementType
    {
        Task<IEnumerable<MeasurementType>> TypesAsync();
        Task<MeasurementType?> FindByIdAsync(Guid id);
    }
}
