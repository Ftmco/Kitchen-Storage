namespace KitchenStorage.Services.Abstraction;

public interface IGetDayFood
{
    Task<PaginationResult<IEnumerable<DayFood>>> DayFoodsAsync(int page, int count, string? q);
}
