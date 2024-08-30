using System.ComponentModel.DataAnnotations;

namespace SA_Online_Mart.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
