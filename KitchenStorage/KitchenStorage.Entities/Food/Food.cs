using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Food : BaseEntity
{
    [Required]
    public string Name { get; set; }

    // Realtions

    public List<Norm> Norms { get; set; }
}
