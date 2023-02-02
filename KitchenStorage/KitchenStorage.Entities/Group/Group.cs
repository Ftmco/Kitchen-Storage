using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities;

public record Group : BaseEntity
{
    [Display(Name = "نام گروه")]
    [Required(ErrorMessage = "لطفا نام گروه را وارد کنید")]
    public string Name { get; set; }
}
