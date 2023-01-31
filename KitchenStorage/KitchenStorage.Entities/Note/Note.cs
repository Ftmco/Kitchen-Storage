using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Note : BaseEntity
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public byte Importance { get; set; }
}
public enum Importance
{
    Low = 0,
    Medium = 1,
    High = 2
}

