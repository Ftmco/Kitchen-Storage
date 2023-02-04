using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Inventory : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public int TypeId { get; set; }
    public virtual MeasurementType MeasurementType { get; set; }

    [Required]
    public string Description { get; set; }
}
