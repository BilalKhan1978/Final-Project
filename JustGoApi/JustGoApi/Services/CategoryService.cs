using JustGoApi.Data;
using JustGoApi.Models;
using JustGoApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace JustGoApi.Services
{
    
    public class CategoryService : ICategoryService
    {
        private readonly ReuseDbContext _dbContext;
        public CategoryService(ReuseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllCategories()
        {
           return await _dbContext.Categories.ToListAsync();
        }
        public async Task<Category> GetOneCategory(int id)
        {
             var category = await _dbContext.Categories.FindAsync(id);
            //var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task AddCategory(AddCategoryRequest addCategoryRequest)
        {
            var category = new Category()
            {
                Name = addCategoryRequest.Name,
                Color = addCategoryRequest.Color,
                backgroundColor = addCategoryRequest.backgroundColor
            };
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null)
              {
                throw new Exception("Not found");
              }
            category.Name = updateCategoryRequest.Name;
            category.Color = updateCategoryRequest.Color;
            category.backgroundColor = updateCategoryRequest.backgroundColor;
            await _dbContext.SaveChangesAsync();
            
        }

    }
}
