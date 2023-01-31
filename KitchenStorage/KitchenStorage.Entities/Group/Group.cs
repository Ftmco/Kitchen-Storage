using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities.Group
{
    public record Group : BaseEntity
    {
        public string Name { get; set; }
    }
}
