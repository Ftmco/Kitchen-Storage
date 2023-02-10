namespace KitchenStorage.ViewModel;

public record MeasurementTypeViewModel(Guid Id,
    string Name,
    string CreateDate,
    byte Status);

public record UpsertMeasurementTypeViewModel(Guid? Id, string Name);

public record ConvertViewModel(Guid Id, double FromValue, double ToValue, MeasurementTypeViewModel? FromType, MeasurementTypeViewModel? ToType);

public record AddConvertViewModel(Guid FromTypeId, Guid ToTypeId, double FromValue, double ToValue);

public enum MeasurementTypeActionStatus
{
    Success = 0,
    Failed = 1,
    NotFound = 2
}
