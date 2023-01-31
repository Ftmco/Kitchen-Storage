﻿using KitchenStorage.Entities.Base;

namespace KitchenStorage.Entities
{
    public record Norm : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Value { get; set; }
    }
}
