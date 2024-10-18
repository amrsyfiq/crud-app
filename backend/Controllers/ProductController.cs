using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudApp.Data;
using CrudApp.Models;
using CrudApp.DTOs;

namespace CrudApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly CrudAppContext _context;

        public ProductController(CrudAppContext context)
        {
            _context = context; // Use the injected CrudAppContext
        }

        // GET: /product
        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            // Convert products to ProductDto
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Brand = p.Brand,
                CategoryId = p.CategoryId,
                Description = p.Description,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                CategoryName = p.Category?.Name // Get the category name if available
            }).ToList();

            return productDtos; // Return the list of ProductDto
        }

        // GET: /product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound(); // Return 404 if not found
            }
            return product; // Return the product if found
        }

        // POST: /product
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product); // Return 201 Created
        }

        // PUT: /product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(); // Return 400 Bad Request if the IDs do not match
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Attempt to save changes
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound(); // Return 404 if the product does not exist
                }
                throw; // Re-throw the exception if another issue occurs
            }

            return NoContent(); // Return 204 No Content on success
        }

        // DELETE: /product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(); // Return 404 if not found
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent(); // Return 204 No Content on success
        }

        private bool ProductExists(int id) => _context.Products.Any(e => e.Id == id); // Helper method to check if product exists
    }
}
