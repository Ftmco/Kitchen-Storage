using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities
{
    public record FoodHistory : BaseEntity
    {
        [Required]
        public Guid FoodId { get; set; }
        public virtual Food Food { get; set; }

        [Required]
        public int Count { get; set; }
    }
}
