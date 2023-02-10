namespace KitchenStorage.Services.Implementation.MeasurmentType;

internal class MeasurementTypeViewModelService : IMeasurementTypeViewModel
{
    private readonly IBaseQuery<MeasurementType> _typeQuery;

    public MeasurementTypeViewModelService(IBaseQuery<MeasurementType> typeQuery)
    {
        _typeQuery = typeQuery;
    }

    public async Task<ConvertViewModel> CreateConvertViewModelAsync(TypeConvert convert)
    {
        MeasurementType? fromType = await _typeQuery.GetAsync(convert.FromTypeId);
        MeasurementType? toType = await _typeQuery.GetAsync(convert.ToTypeId);

        return new(Id: convert.Id,
                    FromValue: convert.FromValue,
                    ToValue: convert.ToValue,
                    FromType: fromType is null ? null : CreateMeasurementTypeViewModel(fromType),
                    ToType: toType is null ? null : CreateMeasurementTypeViewModel(toType));
    }

    public async Task<IEnumerable<ConvertViewModel>> CreateConvertViewModelAsync(IEnumerable<TypeConvert> converts)
    {
        List<ConvertViewModel> viewModels = new();

        foreach (var item in converts)
            viewModels.Add(await CreateConvertViewModelAsync(item));

        return viewModels;
    }

    public MeasurementTypeViewModel CreateMeasurementTypeViewModel(MeasurementType measurementType)
      => new(Id: measurementType.Id,
          Name: measurementType.Name,
          CreateDate: measurementType.CreateDate.ToShamsi(),
          Status: measurementType.Status);

    public IEnumerable<MeasurementTypeViewModel> CreateMeasurementTypeViewModel(IEnumerable<MeasurementType> groups)
        => groups.Select(CreateMeasurementTypeViewModel);
}
