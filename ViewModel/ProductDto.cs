using System.ComponentModel.DataAnnotations;

namespace SA_Online_Mart.ViewModel
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}
