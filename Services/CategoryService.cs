using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    private readonly ILogger<CategoryController> _logger;

    public CategoryService(ICategoryRepository repo, ILogger<CategoryController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public Task<List<Category>> GetCategoryByUserId(string userId)
    {
        _logger.LogInformation("User in service {userId}", userId);
        return _repo.GetCategoryByUserId(userId);
    }

    public async Task<Category> CreateCategory(CreateCategoryDto dto, string userId)
    {

        var thaiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        var category = new Category
        {
            CategoryId = Guid.NewGuid().ToString("N"),
            Name = dto.Name,
            Type = dto.Type,
            UserId = userId,
            IsSystemDefault = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, thaiTimeZone)
        };

        return await _repo.Create(category);
    }

    public async Task<Category> UpdateCategory(CreateCategoryDto dto, string categoryId)
    {

        var category = await _repo.GetCategoryByCategoryId(categoryId);
        if (category == null)
        {
            throw new Exception("Data not found");
        }
        category.Name = dto.Name;
        category.Type = dto.Type;

        return await _repo.Update(category);
    }

    public async Task<bool> DeleteCategory(string categoryId)
    {
        var category = await _repo.GetCategoryByCategoryId(categoryId);
        if (category == null)
        {
            throw new Exception("Data not found");
        }

        return await _repo.Delete(categoryId);
    }
}