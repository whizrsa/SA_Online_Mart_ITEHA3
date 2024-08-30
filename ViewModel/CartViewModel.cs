using SA_Online_Mart.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SA_Online_Mart.ViewModel
{
    public class CartViewModel
    {
        public int CartItemId { get; set; }

        public string? UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Navigation properties
        public AppUser? User { get; set; }
        public Product? Product { get; set; }
    }
}
