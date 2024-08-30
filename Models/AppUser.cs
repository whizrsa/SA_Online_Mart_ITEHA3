using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SA_Online_Mart.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ICollection<Cart> ShoppingCartItems { get; set; }
    }

}
