namespace KitchenStorage.ViewModel;

public record DayViewModel(Guid Id, byte DayOfWeek, string Name, string CreateDate);

public record AddDayViewModel(byte DayOfWeek, string Name);