using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Norm : BaseEntity
{

    [Required]
    public double Value { get; set; }

    [Required]
    public Guid FoodId { get; set; }
    public virtual Food Food { get; set; }
    [Required]
    public Guid InventoryId { get; set; }
    public virtual Inventory Inventory { get; set; }
}
