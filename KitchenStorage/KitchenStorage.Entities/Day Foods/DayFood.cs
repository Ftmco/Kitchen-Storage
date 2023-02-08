namespace KitchenStorage.Entities;

public record DayFood : BaseEntity
{
    [Required]
    public Guid DayId { get; set; }

    [Required]
    public byte Meal { get; set; }

    // Navigation Properties
    // Relationships

    public virtual ICollection<DaysFoods> DaysFoods { get; set; }

    public virtual Day Day { get; set; }
}
