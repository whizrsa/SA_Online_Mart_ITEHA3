using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.ViewModel
{
    [Authorize(Roles = "admin")]
    public class AdminViewModel
    {
        public Product? Product { get; set; }
        public Category? Category { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
