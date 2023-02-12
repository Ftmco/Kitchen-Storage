namespace KitchenStorage.Services.Implementation;

internal class GroupViewModelService : IGroupViewModel
{
    public GroupViewModel CreateGroupViewModel(Group group)
        => new(Id: group.Id,
            Name: group.Name,
            CreateDate: group.CreateDate.ToShamsi());

    public IEnumerable<GroupViewModel> CreateGroupViewModel(IEnumerable<Group> groups)
        => groups.Select(CreateGroupViewModel);
}
