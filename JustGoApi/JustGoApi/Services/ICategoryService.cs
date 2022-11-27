using JustGoApi.Models;
using JustGoApi.ViewModels;

namespace JustGoApi.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetOneCategory(int id);
        Task AddCategory(AddCategoryRequest addCategoryRequest);
        Task UpdateCategory(int id, UpdateCategoryRequest updateCategoryRequest);
    }
}
