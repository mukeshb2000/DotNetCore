using Microsoft.AspNetCore.Mvc;
using ShopManagement.Core.Entities;
using ShopManagement.Core.Interfaces;

namespace ShopManagement.API.Controllers;

/// <summary>
/// Categories controller demonstrating CRUD operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly ILogger<CategoriesController> _logger;

    public CategoriesController(IRepository<Category> categoryRepository, ILogger<CategoriesController> logger)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Category>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        _logger.LogInformation("Getting all categories");
        var categories = await _categoryRepository.GetAllAsync();
        return Ok(categories);
    }

    /// <summary>
    /// Get category by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        _logger.LogInformation("Getting category with ID: {CategoryId}", id);
        
        var category = await _categoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return NotFound($"Category with ID {id} not found");
        }

        return Ok(category);
    }

    /// <summary>
    /// Create a new category
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating new category: {CategoryName}", category.Name);
        
        var createdCategory = await _categoryRepository.CreateAsync(category);
        
        return CreatedAtAction(
            nameof(GetCategory), 
            new { id = createdCategory.Id }, 
            createdCategory);
    }

    /// <summary>
    /// Update an existing category
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] Category category)
    {
        if (id != category.Id)
        {
            return BadRequest("Category ID mismatch");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Updating category with ID: {CategoryId}", id);
        
        var updatedCategory = await _categoryRepository.UpdateAsync(category);
        return Ok(updatedCategory);
    }

    /// <summary>
    /// Delete a category
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        _logger.LogInformation("Deleting category with ID: {CategoryId}", id);
        
        var result = await _categoryRepository.DeleteAsync(id);
        if (!result)
        {
            return NotFound($"Category with ID {id} not found");
        }

        return NoContent();
    }
}