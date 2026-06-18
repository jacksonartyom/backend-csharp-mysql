using System.Text.Json.Serialization;

public class CategoryResponse
{
    [JsonPropertyName("_id")]
    public string? CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
}