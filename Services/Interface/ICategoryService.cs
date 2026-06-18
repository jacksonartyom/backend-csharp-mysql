public interface ICategoryService
{
    Task<List<CategoryResponse>> GetCategoryByUserId(string userId);
    Task<CategoryResponse> CreateCategory(CreateCategoryDto dto, string userId);

    Task<CategoryResponse> UpdateCategory(CreateCategoryDto dto, string categoryId);
    Task<bool> DeleteCategory(string categoryId);

}