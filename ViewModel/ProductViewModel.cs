using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SA_Online_Mart.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public IEnumerable<SelectListItem> Category { get; set; } = default!;
    }
}
