namespace KitchenStorage.Entities;

public record MeasurementType : BaseEntity
{
    [Required]
    public string Name { get; set; }
}

