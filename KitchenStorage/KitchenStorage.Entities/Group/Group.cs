namespace KitchenStorage.Entities;

public record Group : BaseEntity
{
    [Required]
    public string Name { get; set; }
}
