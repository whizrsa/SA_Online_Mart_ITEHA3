using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;
using SA_Online_Mart.ViewModel;

namespace SA_Online_Mart.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Load all categories into a dictionary for quick lookup
            var categoryDictionary = _context.Categories
                                              .ToDictionary(c => c.CategoryId, c => c.CategoryName);

            List<ProductListViewModel> productListViewModelList = new List<ProductListViewModel>();
            var productList = _context.Products.ToList(); // Execute immediately to avoid deferred execution

            if (productList != null)
            {
                foreach (var item in productList)
                {
                    ProductListViewModel productListViewModel = new ProductListViewModel()
                    {
                        Id = item.ProductId,
                        ProductName = item.ProductName,
                        Description = item.Description,
                        Price = item.Price,
                        CategoryId = item.CategoryId,
                        ImageUrl = item.ImageUrl,
                        // Look up the category name from the dictionary
                        CategoryName = categoryDictionary.ContainsKey(item.CategoryId) ? categoryDictionary[item.CategoryId] : "Unknown"
                    };
                    productListViewModelList.Add(productListViewModel);
                }
            }
            return View(productListViewModelList);
        }


        public IActionResult Create()
        {
            ProductViewModel productCreateViewModel = new ProductViewModel();
            productCreateViewModel.Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });

            return View(productCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productCreateViewModel)
        {
            productCreateViewModel.Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            });
            var product = new Product()
            {
                ProductName = productCreateViewModel.ProductName,
                Description = productCreateViewModel.Description,
                Price = productCreateViewModel.Price,
                CategoryId = productCreateViewModel.CategoryId,
                ImageUrl = productCreateViewModel.Image
            };
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["SuccessMsg"] = "Product (" + product.ProductName + ") added successfully.";
                return RedirectToAction("Index");
            }

            return View(productCreateViewModel);
        }

        public IActionResult Edit(int? id)
        {
            var productToEdit = _context.Products.Find(id);
            if (productToEdit != null)
            {
                var productViewModel = new ProductViewModel()
                {
                    Id = productToEdit.ProductId,
                    ProductName = productToEdit.ProductName,
                    Description = productToEdit.Description,
                    Price = productToEdit.Price,
                    CategoryId = productToEdit.CategoryId,
                    Image = productToEdit.ImageUrl,
                    Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    })
                };
                return View(productViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            // Ensure the category list is fetched immediately
            productViewModel.Category = _context.Categories.Select(c => new SelectListItem()
            {
                Text = c.CategoryName,
                Value = c.CategoryId.ToString()
            }).ToList(); // Convert to list to execute immediately

            // Map the ProductViewModel to Product entity
            var product = new Product()
            {
                ProductId = productViewModel.Id,
                ProductName = productViewModel.ProductName,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                CategoryId = productViewModel.CategoryId,
                ImageUrl = productViewModel.Image
            };

            // Ensure that ModelState is valid before updating the product
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the product in the database
                    _context.Products.Update(product);
                    _context.SaveChanges();

                    TempData["SuccessMsg"] = "Product (" + product.ProductName + ") updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception if needed
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the product." + ex);
                }
            }

            // Return the view with the current model if validation or saving fails
            return View(productViewModel);
        }

        public IActionResult Delete(int? id)
        {
            var productToEdit = _context.Products.Find(id);
            if (productToEdit != null)
            {
                var productViewModel = new ProductViewModel()
                {
                    Id = productToEdit.ProductId,
                    ProductName = productToEdit.ProductName,
                    Description = productToEdit.Description,
                    Price = productToEdit.Price,
                    CategoryId = productToEdit.CategoryId,
                    Image = productToEdit.ImageUrl,
                    Category = (IEnumerable<SelectListItem>)_context.Categories.Select(c => new SelectListItem()
                    {
                        Text = c.CategoryName,
                        Value = c.CategoryId.ToString()
                    })
                };
                return View(productViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int? id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            TempData["SuccessMsg"] = "Product (" + product.ProductName + ") deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
