namespace KitchenStorage.Services.Implementation;

internal class DayViewModelService : IDayViewModel
{
    public DayViewModel CreateDayViewModel(Day day)
            => new(Id: day.Id,
                DayOfWeek: day.DayOfWeek,
                Name: day.Name,
                CreateDate: day.CreateDate.ToShamsi());

    public IEnumerable<DayViewModel> CreateDayViewModel(IEnumerable<Day> days)
        => days.Select(CreateDayViewModel);
}
