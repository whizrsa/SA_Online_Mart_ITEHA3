using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SA_Online_Mart.Models;
using SA_Online_Mart.Services;

namespace SA_Online_Mart.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categoryList = await _categoryService.GetAllCategories();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateCategory(category);
                TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") added successfully.";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategory(category);

                TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") updated successfully.";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound("Category does not exist!");
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? categoryId, Category category)
        {
            if (categoryId == null)
            {
                //return NotFound("Category does not exist!");
                return RedirectToAction("Index", "Category");
            }

            var existingCategory = await _categoryService.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategory(category);

            TempData["SuccessMsg"] = "Category (" + category.CategoryName + ") deleted successfully.";
            return RedirectToAction("Index", "Category");
        }
    }
}
