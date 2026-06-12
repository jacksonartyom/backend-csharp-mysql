public interface ICategoryService
{
    Task<List<Category>> GetCategoryByUserId(string userId);
    Task<Category> CreateCategory(CreateCategoryDto dto, string userId);

    Task<Category> UpdateCategory(CreateCategoryDto dto, string categoryId);
    Task<bool> DeleteCategory(string categoryId);

}