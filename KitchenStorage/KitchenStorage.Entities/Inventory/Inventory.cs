using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities
{
    public record Inventory : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public byte Type { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
