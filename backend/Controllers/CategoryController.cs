using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudApp.Data;
using CrudApp.Models;

namespace CrudApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CrudAppContext _context;

        public CategoryController(CrudAppContext context)
        {
            _context = context; // Use the injected CrudAppContext
        }

        // GET: /api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
