namespace KitchenStorage.Services.Abstraction;

public interface IGetMeasurementType
{
    Task<PaginationResult<IEnumerable<MeasurementType>>> TypesAsync(int page, int count, string? q);

    Task<IEnumerable<TypeConvert>> ConversionsAsync(Guid id);
}
