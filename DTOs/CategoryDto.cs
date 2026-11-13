namespace FreakyFashionAPI.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Image { get; set; } = "";
    public string UrlSlug { get; set; } = "";

    public List<ProductDto> Products { get; set; } = new();
}