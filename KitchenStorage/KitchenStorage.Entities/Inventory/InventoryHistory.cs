namespace KitchenStorage.Entities;

public record InventoryHistory : BaseEntity
{
    [Required]
    public Guid InventoryId { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public double NextValue { get; set; }
}
