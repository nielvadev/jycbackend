﻿using System.ComponentModel.DataAnnotations;

namespace jycbackend.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }

    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int Stock { get; set; } = 0;
    }
}
