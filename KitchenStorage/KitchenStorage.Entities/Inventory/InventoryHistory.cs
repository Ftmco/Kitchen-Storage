namespace KitchenStorage.Entities;

public record InventoryHistory : BaseEntity
{
    [Required]
    public Guid GenerateId { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public byte Type { get; set; }

    // Navigation Properties
    // Relationships

    public virtual ICollection<InventoryHistoryList> InventoryHistoryList { get; set; }
}

public record InventoryHistoryList : BaseEntity
{
    [Required]
    public Guid HistoryId { get; set; }

    [Required]
    public Guid InventoryId { get; set; }

    [Required]
    public double Value { get; set; }

    [Required]
    public double InventoryValue { get; set; }

    // Navigation Properties
    // Relationships

    public virtual InventoryHistory InventoryHistory { get; set; }
}

public enum InventoryHistoryType
{
    Input = 0,
    Output = 1,
}