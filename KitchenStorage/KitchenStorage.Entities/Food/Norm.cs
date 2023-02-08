namespace KitchenStorage.Entities;

public record Norm : BaseEntity
{

    [Required]
    public double Value { get; set; }

    public Guid InventoryId { get; set; }

    // Navigation Properties
    // Relationships

    public virtual Inventory Inventory { get; set; }

    public virtual Food Food { get; set; }
}
