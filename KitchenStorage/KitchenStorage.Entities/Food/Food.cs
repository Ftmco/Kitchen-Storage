namespace KitchenStorage.Entities;

public record Food : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]

    public byte Type { get; set; }

    // Relationships

    public virtual ICollection<Norm> Norms { get; set; }

    public virtual ICollection<DaysFoods> DaysFoods { get; set; }
}
