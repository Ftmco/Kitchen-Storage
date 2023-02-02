namespace KitchenStorage.Services.Abstraction;

public interface IGroupViewModel
{
    GroupViewModel CreateGroupViewModel(Group group);

    IEnumerable<GroupViewModel> CreateGroupViewModel(IEnumerable<Group> groups);
}
