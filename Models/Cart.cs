using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SA_Online_Mart.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();

    }
}
