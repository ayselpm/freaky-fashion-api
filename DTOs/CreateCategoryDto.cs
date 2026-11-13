using System.ComponentModel.DataAnnotations;

namespace FreakyFashionAPI.DTOs;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; } = "";

    public string Image { get; set; } = "";
}