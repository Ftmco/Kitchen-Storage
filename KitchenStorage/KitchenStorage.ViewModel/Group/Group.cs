namespace KitchenStorage.ViewModel;

public record GroupViewModel(Guid Id, string Name, string CreateDate);

public record UpsertGroupViewModel(Guid? Id, string Name);