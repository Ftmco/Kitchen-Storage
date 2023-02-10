namespace KitchenStorage.Entities;

[Index(nameof(FoodId))]
public record FoodHistory : BaseEntity
{
    [Required]
    public Guid DayId { get; set; }

    [Required]
    public Guid FoodId { get; set; }

    [Required]
    public int Count { get; set; }

    [Required]
    public string Meal { get; set; }

    public string Description { get; set; }

    // Navigation Properties
    // Relationships

    public virtual Food Food { get; set; }

    public virtual Day Day { get; set; }
}
