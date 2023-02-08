namespace KitchenStorage.Entities;

public record DaysFoods : BaseEntity
{
    [Required]
    public Guid DayFoodId { get; set; }

    [Required]
    public Guid FoodId { get; set; }

    // Navigation Properties
    // Relationships

    public virtual Food Food { get; set; }

    public virtual DayFood DayFood { get; set; }
}
