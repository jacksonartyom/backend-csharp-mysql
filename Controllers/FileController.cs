using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/upload")]
public class FileController : ControllerBase
{
    private readonly IUploadService _service;
    public FileController(IUploadService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost("profile")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("UserId not found in token");
        }

        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty");
        }

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var ext = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(ext))
        {
            return BadRequest("Invalid file type");
        }

        var maxSize = 5 * 1024 * 1024;
        if (file.Length > maxSize)
        {
            return BadRequest("File too large (max 5MB)");
        }

        try
        {
            var uploadDir = "uploads";

            // กัน path traversal
            var originalFileName = Path.GetFileName(file.FileName);

            var fileName = $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}_{originalFileName}";

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), uploadDir);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"http://localhost:5024/files/{fileName}";

            return Ok(new
            {
                message = "success",
                url = fileUrl
            });
        }
        catch (Exception)
        {
            return StatusCode(500, "Upload failed");
        }
    }

    [Authorize]
    [HttpPost("transaction")]
    public async Task<IActionResult> UploadExcel([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty");
        }

        // ✅ จำกัด Excel เท่านั้น
        var allowedExtensions = new[] { ".xlsx", ".xls" };
        var ext = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(ext))
        {
            return BadRequest("Invalid file type (only Excel allowed)");
        }

        try
        {
            await _service.ReadExcel(file);

            return Ok(new
            {
                message = "success",
                result = "Create transaction from excel success"
            });
        }
        catch (Exception e)
        {
            return BadRequest($"Upload failed: {e.Message}");
        }
    }
}