namespace KitchenStorage.ViewModel;

public record DayViewModel(Guid Id, byte DayOfWeek, string Name, string CreateDate);

public record AddDayViewModel(byte DayOfWeek, string Name);

public enum DayActionStatus
{
    Success = 0,
    Failed = 1,
    NotFound = 2,
    Exist = 3,
}