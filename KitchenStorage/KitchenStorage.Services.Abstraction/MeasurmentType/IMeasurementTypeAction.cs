namespace KitchenStorage.Services.Abstraction;

public interface IMeasurementTypeAction
{
    Task<Either<MeasurementTypeActionStatus, MeasurementType>> UpsertAsync(UpsertMeasurementTypeViewModel upsert);

    Task<Either<MeasurementTypeActionStatus, MeasurementType>> CreateAsync(UpsertMeasurementTypeViewModel upsert);

    Task<Either<MeasurementTypeActionStatus, MeasurementType>> UpdateAsync(UpsertMeasurementTypeViewModel upsert);

    Task<MeasurementTypeActionStatus> DeleteAsync(Guid id);

    Task<Either<MeasurementTypeActionStatus, TypeConvert>> AddConvertAsync(AddConvertViewModel addConvert);

    Task<MeasurementTypeActionStatus> DeleteConvertAsync(Guid id);
}
