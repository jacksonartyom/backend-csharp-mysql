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

    public async Task<List<CategoryResponse>> GetCategoryByUserId(string userId)
    {
        var result = await _repo.GetCategoryByUserId(userId);
        var responseList = new List<CategoryResponse>();
        foreach (var item in result)
        {
            var response = new CategoryResponse
            {
                CategoryId = item.CategoryId,
                Name = item.Name,
                Type = item.Type
            };

            responseList.Add(response);
        }
        return responseList;
    }

    public async Task<CategoryResponse> CreateCategory(CreateCategoryDto dto, string userId)
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

        await _repo.Create(category);

        var CategoryResponse = new CategoryResponse
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Type = category.Type
        };

        return CategoryResponse;
    }

    public async Task<CategoryResponse> UpdateCategory(CreateCategoryDto dto, string categoryId)
    {

        var category = await _repo.GetCategoryByCategoryId(categoryId);
        if (category == null)
        {
            throw new Exception("Data not found");
        }
        category.Name = dto.Name;
        category.Type = dto.Type;
        await _repo.Update(category);

        var categoryResponse = new CategoryResponse
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Type = category.Type
        };

        return categoryResponse;
    }

    public async Task<bool> DeleteCategory(string categoryId)
    {
        var category = await _repo.GetCategoryByCategoryId(categoryId);
        if (category == null)
        {
            return false;
        }
        return await _repo.Delete(categoryId);
    }
}