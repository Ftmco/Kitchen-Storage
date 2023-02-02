namespace KitchenStorage.Services.Implementation;

internal class GroupViewModelService : IGroupViewModel
{
    public GroupViewModel CreateGroupViewModel(Group group)
        => new(Id: group.Id,
            Name: group.Name,
            CreateDate: group.CreateDate.ToShamsi(),
            Status: group.Status);

    public IEnumerable<GroupViewModel> CreateGroupViewModel(IEnumerable<Group> groups)
        => groups.Select(CreateGroupViewModel);
}
