namespace KitchenStorage.Entities;

[Index(nameof(FoodId))]
public record FoodHistory : BaseEntity
{
    [Required]
    public Guid FoodId { get; set; }

    [Required]
    public int Count { get; set; }

    [Required]
    public byte Meal { get; set; }


    // Navigation Properties
    // Relationships

    public virtual Food Food { get; set; }

}
