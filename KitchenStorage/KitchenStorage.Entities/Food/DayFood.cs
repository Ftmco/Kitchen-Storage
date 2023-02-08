namespace KitchenStorage.Entities;

public record DayFood : BaseEntity
{
    [Required]
    public byte DayNumber { get; set; }

    [Required]
    public string Day { get; set; }

    [Required]
    public byte Meal { get; set; }

    // Navigation Properties
    // Relationships

    public virtual ICollection<DaysFoods> DaysFoods { get; set; }
}
