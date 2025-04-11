using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SA_Online_Mart.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Precision(16, 2)]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [MaxLength(150)]
        public string ImageFileName { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Navigation property
        [Required]
        public Category? Category { get; set; }
        public ICollection<Cart>? ShoppingCartItems { get; set; } = new List<Cart>();
    }
}
