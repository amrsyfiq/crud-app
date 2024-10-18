using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudApp.Data; // Adjust this namespace according to your project
using CrudApp.Models; // Ensure to include your Category model namespace

public class CategorySeeder
{
    public static async Task SeedCategoriesAsync(CrudAppContext context)
    {
        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new Category { Name = "Electronics" },
                new Category { Name = "Clothing" },
                new Category { Name = "Books" },
                new Category { Name = "Home & Kitchen" },
                new Category { Name = "Sports" },
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }
    }
}
