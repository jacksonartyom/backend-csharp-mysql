using Microsoft.EntityFrameworkCore;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategoryByUserId(string userId)
    {
        return await _context.Category.Where(w => w.UserId == userId || w.IsSystemDefault).ToListAsync();
    }


    public async Task<Category> Create(Category category)
    {
        _context.Category.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> GetCategoryByCategoryId(string categoryId)
    {
        return await _context.Category.FirstOrDefaultAsync(w => w.CategoryId == categoryId);
    }

    public async Task<Category> Update(Category category)
    {
        _context.Category.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<bool> Delete(string categoryId)
    {
        var category = await _context.Category
            .FirstOrDefaultAsync(w => w.CategoryId == categoryId);

        if (category == null)
            return false;

        _context.Category.Remove(category);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}