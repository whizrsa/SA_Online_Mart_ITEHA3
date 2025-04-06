using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;
using SA_Online_Mart.Services;
using SA_Online_Mart.ViewModel;
using System;
using System.Threading.Tasks;

namespace SA_Online_Mart.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            this._productService = productService;
            this._hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var products =  await _productService.GetAllProducts();

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _productService.GetCategories();
            ViewBag.Categories = new SelectList(categories,"CategoryId", "CategoryName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var categories = await _productService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            if(productDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Please upload an image.");
            }

            if(!ModelState.IsValid)
            {
                return View(productDto);
            }

            // Save the image
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDto.ImageFile!.FileName);

            string imageFullPath = _hostingEnvironment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDto.ImageFile.CopyTo(stream);
            }

            //Save the new product in the database
            Product product = new Product()
            {
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                ImageFileName = newFileName,
                DateAdded = DateTime.Now,
            };

            await _productService.Create(product);
            TempData["SuccessMsg"] = "Product (" + product.ProductName + ") created successfully!";
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productService.FindId(id);

            if (product == null)
            {
                return NotFound();
            }

            var categories = await _productService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

            var productDto = new ProductDto()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,

            };

            ViewData["ImageFileName"] = product.ImageFileName;
            ViewData["ProductId"] = id;
            ViewData["DateAdded"] = product.DateAdded;

            return View(productDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductDto productDto)
        {
            var product = await _productService.FindId(id);

            if (product == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewData["ImageFileName"] = product.ImageFileName;
                ViewData["ProductId"] = id;
                ViewData["DateAdded"] = product.DateAdded.ToString("MM/dd/yyyy");
                return View(productDto);
            }

            //update the image file if we have a new image file
            string newFileName = product.ImageFileName;
            if (productDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(productDto.ImageFile.FileName);

                string imageFullPath = _hostingEnvironment.WebRootPath + "/images/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    productDto.ImageFile.CopyTo(stream);
                }

                //delete the old image
                string oldImageFullPath = _hostingEnvironment.WebRootPath + "/images/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            // update the product in the database
            product.ProductName = productDto.ProductName;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.ImageFileName = newFileName;
            product.CategoryId = productDto.CategoryId; //to update the Category

            await _productService.UpdateProduct(product);

            return RedirectToAction("Index", "Product");
            
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToEdit = await _productService.FindId(id);

            if (productToEdit == null)
            {
                return NotFound();
            }
            return View(productToEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductDto productDto)
        {
            var product = await _productService.FindId(id);

            if (product == null)
            {
                return RedirectToAction("Index", "Product");
            }

            // Delete image
            string imageFullPath = _hostingEnvironment.WebRootPath + "/images/" + product.ImageFileName;
            System.IO.File.Delete(imageFullPath);
            
            await _productService.DeleteProduct(product);
            TempData["SuccessMsg"] = "Product (" + product.ProductName + ") deleted successfully.";
            return RedirectToAction("Index", "Product");
        }
    }
}
