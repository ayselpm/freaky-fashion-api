
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FreakyFashionAPI.Data;
using FreakyFashionAPI.DTOs;
using FreakyFashionAPI.Models;
using System.Text.RegularExpressions;

namespace FreakyFashionAPI.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    // Get all categories or filter by slug
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories([FromQuery] string? slug)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(slug))
        {
            query = query.Where(c => c.UrlSlug == slug);
        }

        var categories = await query
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.Image,
                UrlSlug = c.UrlSlug,
                Products = c.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = p.Image,
                    UrlSlug = p.UrlSlug
                }).ToList()
            })
            .ToListAsync();

        return Ok(categories);
    }

    // Get a category by ID
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var category = await _context.Categories
            .Where(c => c.Id == id)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Image = c.Image,
                UrlSlug = c.UrlSlug,
                Products = c.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = p.Image,
                    UrlSlug = p.UrlSlug
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (category == null)
            return NotFound();

        return Ok(category);
    }

    // Create a new category
    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CreateCategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return BadRequest("Name is required.");

        var slug = dto.Name.Trim().ToLowerInvariant();
        slug = Regex.Replace(slug, @"[^a-z0-9]+", "-").Trim('-');

        if (string.IsNullOrEmpty(slug))
            return BadRequest("Name must contain letters or numbers.");

        var category = new Category
        {
            Name = dto.Name.Trim(),
            Image = dto.Image,
            UrlSlug = slug
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var result = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Image = category.Image,
            UrlSlug = category.UrlSlug,
            Products = new List<ProductDto>()
        };

        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, result);
    }

    // Delete a category by ID
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
            return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
