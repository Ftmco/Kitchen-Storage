namespace KitchenStorage.Services.Abstraction;

public interface IMeasurementTypeViewModel
{
    MeasurementTypeViewModel CreateMeasurementTypeViewModel(MeasurementType measurementType);

    IEnumerable<MeasurementTypeViewModel> CreateMeasurementTypeViewModel(IEnumerable<MeasurementType> MeasurementType);

    Task<ConvertViewModel> CreateConvertViewModelAsync(TypeConvert convert);

    Task<IEnumerable<ConvertViewModel>> CreateConvertViewModelAsync(IEnumerable<TypeConvert> converts);
}
