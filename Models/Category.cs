namespace FreakyFashionAPI.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Image { get; set; } = "";
    public string UrlSlug { get; set; } = "";

    public List<Product> Products { get; set; } = new();
}