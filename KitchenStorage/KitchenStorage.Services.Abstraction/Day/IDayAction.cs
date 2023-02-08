namespace KitchenStorage.Services.Abstraction;

public interface IDayAction
{
    Task<Either<DayActionStatus, Day>> CreateAsync(AddDayViewModel addDay);

    Task<DayActionStatus> DeleteAsync(Guid id);
}

