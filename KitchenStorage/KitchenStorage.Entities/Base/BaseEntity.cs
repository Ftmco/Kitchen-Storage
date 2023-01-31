namespace KitchenStorage.Entities.Base;

public record BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public byte Status { get; set; }
}

public enum EntityStatus
{
    Active = 0,
    DeActive = 1,
    Deleted = 2,
}
