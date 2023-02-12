namespace KitchenStorage.Entities;

[Index(nameof(Id), nameof(GroupId), nameof(TypeId))]
public record Inventory : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public double AlertLimit { get; set; }

    [Required]
    public Guid TypeId { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public Guid GroupId { get; set; }

    // Navigation Properties
    // Relationships

    public virtual MeasurementType MeasurementType { get; set; }

    public virtual ICollection<InventoryPartition> Partitions { get; set; }

    public virtual ICollection<Norm> Norms { get; set; }

}

public enum InventoryStatus
{
    Available = 0,
    AlertLimit = 1,
    Finished = 2,
}
