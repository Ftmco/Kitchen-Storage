using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Group : BaseEntity
{
    [Required]
    public string Name { get; set; }

    public virtual List<Inventory> Inventories { get; set; }
}
