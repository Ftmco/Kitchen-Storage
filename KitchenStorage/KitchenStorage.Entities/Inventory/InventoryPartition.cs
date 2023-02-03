using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities
{
    public record InventoryPartition : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Value { get; set; }

        [Required]
        public Guid InventoryId { get; set; }
        public virtual Inventory Partition { get; set; }
    }
}
