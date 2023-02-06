using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Group : BaseEntity
{
    [Required]
    public string Name { get; set; }

    // Navigation Properties
    // Relationships

    public virtual ICollection<Inventory> Inventories { get; set; }
}
