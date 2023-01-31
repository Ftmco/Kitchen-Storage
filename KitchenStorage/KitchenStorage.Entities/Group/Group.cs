using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Group : BaseEntity
{
    public string Name { get; set; }
}
