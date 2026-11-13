using System.ComponentModel.DataAnnotations;

namespace FreakyFashionAPI.DTOs;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    public string Image { get; set; } = "";

    public int? CategoryId { get; set; }
}