using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Food : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]

    public byte Type { get; set; }

    // Realtions
    public virtual List<Norm> Norms { get; set; }
}
