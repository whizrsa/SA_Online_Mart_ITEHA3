using System.ComponentModel.DataAnnotations;

namespace SA_Online_Mart.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }

        // Navigation property
        public ICollection<Product>? Products { get; set; }
    }
}
