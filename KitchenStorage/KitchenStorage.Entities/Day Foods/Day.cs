namespace KitchenStorage.Entities;

public record Day : BaseEntity
{
    [Required]
    public byte DayOfWeek { get; set; }

    [Required]
    public string Name { get; set; }

    // Navigation Properties
    // Relationships

    public virtual ICollection<DayFood> DayFood { get; set; }

    public virtual ICollection<FoodHistory> FoodHistories { get; set; }

}
