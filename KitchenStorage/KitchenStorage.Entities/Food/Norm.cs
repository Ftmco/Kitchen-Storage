using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Norm : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public double Value { get; set; }

    public Guid FoodId { get; set; }
    public virtual Food Food { get; set; }
    public Guid InventoryId { get; set; }
    public virtual Inventory Inventory { get; set; }
}
