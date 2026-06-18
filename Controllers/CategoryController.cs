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

        var response = new ResponseDto<List<CategoryResponse>>
        {
            Message = "success",
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
        var response = new ResponseDto<CategoryResponse>
        {
            Message = "success",
            Result = result
        };
        return Ok(response);
    }

    [Authorize]
    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> Delete(string categoryId)
    {
        var category = await _service.DeleteCategory(categoryId);
        if (category)
        {
            var response = new ResponseDto<string>
            {
                Message = "success",
                Result = "Delete category success"
            };

            return Ok(response);
        }
        else
        {
            return BadRequest(new { message = "Delete category fail" });
        }
        ;
    }

}