namespace KitchenStorage.Services.Abstraction;

public interface IGetDay
{
    Task<IEnumerable<Day>> DaysAsync();
}
