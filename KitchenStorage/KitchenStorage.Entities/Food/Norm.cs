namespace KitchenStorage.Entities;

public record Norm : BaseEntity
{

    [Required]
    public double Value { get; set; }

    [Required]
    public Guid FoodId { get; set; }

    [Required]
    public Guid InventoryId { get; set; }

    [Required]
    public Guid TypeId { get; set; }

    // Navigation Properties
    // Relationships

    public virtual Inventory Inventory { get; set; }

    public virtual Food Food { get; set; }
}
