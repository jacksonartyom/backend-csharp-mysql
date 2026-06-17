using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("UserId not found in token");
        }
        var result = await _service.GetCategoryByUserId(userId);

        var response = new ResponseDto<List<Category>>
        {
            Message = "Get category: Success",
            Result = result
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("UserId not found in token");
        }
        var result = await _service.CreateCategory(dto, userId);
        var response = new ResponseDto<Category>
        {
            Message = "Create category: Success",
            Result = result
        };
        return Ok(response);
    }

    [Authorize]
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> Delete(string categoryId)
    {
        var category = await _service.DeleteCategory(categoryId);
        var response = new ResponseDto<bool>
        {
            Message = "Delete category: Success",
            Result = category
        };
        return Ok(response);
    }

}