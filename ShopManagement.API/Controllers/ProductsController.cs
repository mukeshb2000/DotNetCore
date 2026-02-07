using Microsoft.AspNetCore.Mvc;
using ShopManagement.Core.Entities;
using ShopManagement.Core.Interfaces;

namespace ShopManagement.API.Controllers;

/// <summary>
/// Products controller demonstrating RESTful API design and CRUD operations
/// Uses dependency injection to access the product service
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    // Constructor injection demonstrating Dependency Injection
    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Get all products
    /// </summary>
    /// <returns>List of products</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        _logger.LogInformation("Getting all products");
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Get product by ID
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Product details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        _logger.LogInformation("Getting product with ID: {ProductId}", id);
        
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", id);
            return NotFound($"Product with ID {id} not found");
        }

        return Ok(product);
    }

    /// <summary>
    /// Create a new product
    /// </summary>
    /// <param name="product">Product to create</param>
    /// <returns>Created product</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating new product: {ProductName}", product.Name);
        
        var createdProduct = await _productService.CreateProductAsync(product);
        
        _logger.LogInformation("Product created with ID: {ProductId}", createdProduct.Id);
        
        return CreatedAtAction(
            nameof(GetProduct), 
            new { id = createdProduct.Id }, 
            createdProduct);
    }

    /// <summary>
    /// Update an existing product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="product">Updated product data</param>
    /// <returns>Updated product</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
    {
        if (id != product.Id)
        {
            return BadRequest("Product ID mismatch");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Updating product with ID: {ProductId}", id);
        
        var updatedProduct = await _productService.UpdateProductAsync(product);
        
        _logger.LogInformation("Product updated: {ProductId}", id);
        
        return Ok(updatedProduct);
    }

    /// <summary>
    /// Delete a product
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <returns>Success status</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        _logger.LogInformation("Deleting product with ID: {ProductId}", id);
        
        var result = await _productService.DeleteProductAsync(id);
        if (!result)
        {
            _logger.LogWarning("Product with ID {ProductId} not found for deletion", id);
            return NotFound($"Product with ID {id} not found");
        }

        _logger.LogInformation("Product deleted: {ProductId}", id);
        return NoContent();
    }

    /// <summary>
    /// Get products by category
    /// </summary>
    /// <param name="categoryId">Category ID</param>
    /// <returns>List of products in the category</returns>
    [HttpGet("category/{categoryId}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int categoryId)
    {
        _logger.LogInformation("Getting products for category: {CategoryId}", categoryId);
        
        var products = await _productService.GetProductsByCategoryAsync(categoryId);
        return Ok(products);
    }

    /// <summary>
    /// Get products that are in stock
    /// </summary>
    /// <returns>List of in-stock products</returns>
    [HttpGet("in-stock")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetInStockProducts()
    {
        _logger.LogInformation("Getting in-stock products");
        
        var products = await _productService.GetInStockProductsAsync();
        return Ok(products);
    }

    /// <summary>
    /// Get products with low stock
    /// </summary>
    /// <param name="threshold">Stock threshold (default: 10)</param>
    /// <returns>List of low-stock products</returns>
    [HttpGet("low-stock")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetLowStockProducts([FromQuery] int threshold = 10)
    {
        _logger.LogInformation("Getting low-stock products with threshold: {Threshold}", threshold);
        
        var products = await _productService.GetLowStockProductsAsync(threshold);
        return Ok(products);
    }

    /// <summary>
    /// Search products by name
    /// </summary>
    /// <param name="searchTerm">Search term</param>
    /// <returns>List of matching products</returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string searchTerm)
    {
        _logger.LogInformation("Searching products with term: {SearchTerm}", searchTerm);
        
        var products = await _productService.SearchProductsAsync(searchTerm);
        return Ok(products);
    }

    /// <summary>
    /// Update product stock
    /// </summary>
    /// <param name="id">Product ID</param>
    /// <param name="quantity">Quantity to add/subtract</param>
    /// <returns>Success status</returns>
    [HttpPatch("{id}/stock")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProductStock(int id, [FromBody] int quantity)
    {
        _logger.LogInformation("Updating stock for product {ProductId} by {Quantity}", id, quantity);
        
        var result = await _productService.UpdateProductStockAsync(id, quantity);
        if (!result)
        {
            return NotFound($"Product with ID {id} not found");
        }

        _logger.LogInformation("Stock updated for product: {ProductId}", id);
        return Ok(new { message = "Stock updated successfully" });
    }
}