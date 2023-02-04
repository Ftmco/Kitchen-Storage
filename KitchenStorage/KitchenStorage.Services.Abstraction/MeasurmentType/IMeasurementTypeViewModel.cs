namespace KitchenStorage.Services.Abstraction
{
    public interface IMeasurementTypeViewModel
    {
        MeasurementTypeViewModel CreateMeasurementTypeViewModel(MeasurementType measurementType);

        IEnumerable<MeasurementTypeViewModel> CreateMeasurementTypeViewModel(IEnumerable<MeasurementType> MeasurementType);
    }
}
