using Microsoft.EntityFrameworkCore;
using SA_Online_Mart.Data;
using SA_Online_Mart.Models;

namespace SA_Online_Mart.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> DeleteCategory(Category category)
        {
            var findCategory = await _context.Categories.FindAsync(category.CategoryId);

            if(findCategory == null)
            {
                return null; 
            }

            _context.Categories.Remove(findCategory);
            await _context.SaveChangesAsync();

            return findCategory;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category> GetCategoryById(int? categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (categoryId == null)
            {
                return null;
            }

            return category;
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.CategoryId);

            if(existingCategory == null)
            {
                return null;
            }

            _context.Entry(existingCategory).CurrentValues.SetValues(category);
            await _context.SaveChangesAsync();

            return existingCategory;
        }
    }
}
