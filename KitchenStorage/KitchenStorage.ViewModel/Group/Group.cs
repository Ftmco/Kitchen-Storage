namespace KitchenStorage.ViewModel;

public record GroupViewModel(Guid Id, string Name, string CreateDate, byte Status);

public record UpsertGroupViewModel(Guid? Id, string Name);

public enum GroupActionStatus
{
    Success = 0,
    Failed = 1,
    NotFound = 2
}