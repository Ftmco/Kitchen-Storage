namespace KitchenStorage.ViewModel
{
    public record MeasurementTypeViewModel(Guid Id,
        string Name,
        string CreateDate,
        byte Status);

    public record UpsertMeasurementTypeViewModel(Guid? Id, string Name);
    public enum MeasurementTypeActionStatus
    {
        Success = 0,
        Failed = 1,
        NotFound = 2
    }
}
