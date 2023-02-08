namespace KitchenStorage.Services.Implementation.MeasurmentType;

public class MeasurementTypeAction : IMeasurementTypeAction
{
    private readonly IBaseQuery<MeasurementType> _query;
    private readonly IBaseCud<MeasurementType> _action;

    private readonly IBaseQuery<TypeConvert> _convertQuery;
    private readonly IBaseCud<TypeConvert> _convertCud;

    public MeasurementTypeAction
        (IBaseQuery<MeasurementType> query,
        IBaseCud<MeasurementType> action,
        IBaseCud<TypeConvert> convertCud,
        IBaseQuery<TypeConvert> convertQuery)
    {
        _query = query;
        _action = action;
        _convertCud = convertCud;
        _convertQuery = convertQuery;
    }

    public async Task<Either<MeasurementTypeActionStatus, TypeConvert>> AddConvertAsync(AddConvertViewModel addConvert)
    {
        TypeConvert convert = new()
        {
            FromTypeId = addConvert.FromTypeId,
            ToTypeId = addConvert.ToTypeId,
            FromValue = addConvert.FromValue,
            ToValue = addConvert.ToValue
        };
        return await _convertCud.InsertAsync(convert) ?
                                convert : MeasurementTypeActionStatus.Failed;
    }

    public async Task<Either<MeasurementTypeActionStatus, MeasurementType>> CreateAsync(UpsertMeasurementTypeViewModel upsert)
    {
        MeasurementType newMeasurement = new()
        {
            Name = upsert.Name,
        };

        return await _action.InsertAsync(newMeasurement) ?
                    newMeasurement : MeasurementTypeActionStatus.Failed;
    }

    public async Task<MeasurementTypeActionStatus> DeleteAsync(Guid id)
            => await _action.DeleteAsync(id)
                        ? MeasurementTypeActionStatus.Success
                            : MeasurementTypeActionStatus.Failed;

    public async Task<MeasurementTypeActionStatus> DeleteConvertAsync(Guid id)
            => await _convertCud.DeleteAsync(id)
                                ? MeasurementTypeActionStatus.Success
                                : MeasurementTypeActionStatus.Failed;

    public async Task<Either<MeasurementTypeActionStatus, MeasurementType>> UpdateAsync(UpsertMeasurementTypeViewModel upsert)
    {
        var measurement = await _query.GetAsync(upsert.Id);
        if (measurement is null)
            return MeasurementTypeActionStatus.NotFound;

        measurement.Name = upsert.Name;
        return await _action.UpdateAsync(measurement) ?
        measurement : MeasurementTypeActionStatus.Failed;
    }

    public async Task<Either<MeasurementTypeActionStatus, MeasurementType>> UpsertAsync(UpsertMeasurementTypeViewModel upsert)
            => upsert.Id is null
            ? await CreateAsync(upsert)
            : await UpdateAsync(upsert);
}
