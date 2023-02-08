namespace KitchenStorage.Entities;

[Index(nameof(FromTypeId), nameof(ToTypeId))]
public record TypeConvert : BaseEntity
{
    [Required]
    public Guid FromTypeId { get; set; }

    [Required]
    public Guid ToTypeId { get; set; }

    [Required]
    public double FromValue { get; set; }

    [Required]
    public double ToValue { get; set; }
}
