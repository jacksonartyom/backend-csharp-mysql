public interface ICategoryRepository
{
        Task<List<Category>> GetCategoryByUserId(string userId);

        Task<Category> Create(Category category);

        Task<Category?> GetCategoryByCategoryId(string category);

        Task<Category> Update(Category category);
        Task<bool> Delete(string categoryId);
}