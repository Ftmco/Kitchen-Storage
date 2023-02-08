namespace KitchenStorage.Entities;

public record DayFood : BaseEntity
{
    [Required]
    public Guid DayId { get; set; }

    [Required]
    public Guid FoodId { get; set; }

    [Required]
    public string Meal { get; set; }

    // Navigation Properties
    // Relationships

    public virtual Day Day { get; set; }
}
