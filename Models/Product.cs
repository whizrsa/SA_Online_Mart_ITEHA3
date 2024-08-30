using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace SA_Online_Mart.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string? ImageUrl { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Navigation property
        [Required]
        public Category? Category { get; set; }
        public ICollection<Cart>? ShoppingCartItems { get; set; } = new List<Cart>();
    }
}
