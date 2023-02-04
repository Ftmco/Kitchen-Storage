namespace KitchenStorage.Services.Implementation.MeasurmentType
{
    public class MeasurementTypeViewModelService : IMeasurementTypeViewModel
    {
        public MeasurementTypeViewModel CreateMeasurementTypeViewModel(MeasurementType measurementType)
          => new(Id: measurementType.Id,
              Name: measurementType.Name,
              CreateDate: measurementType.CreateDate.ToShamsi(),
              Status: measurementType.Status);

        public IEnumerable<MeasurementTypeViewModel> CreateMeasurementTypeViewModel(IEnumerable<MeasurementType> groups)
            => groups.Select(CreateMeasurementTypeViewModel);
    }
}
