namespace KitchenStorage.Entities;

[Index(nameof(InventoryId))]
public record InventoryPartition : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public Guid InventoryId { get; set; }

    //Navigation Properties
    //Relationships

    public virtual Inventory Inventory { get; set; }
}
