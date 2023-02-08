namespace KitchenStorage.Services.Abstraction;

public interface IDayViewModel
{
    DayViewModel CreateDayViewModel(Day day);

    IEnumerable<DayViewModel> CreateDayViewModel(IEnumerable<Day> days);
}
