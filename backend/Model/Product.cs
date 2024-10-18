// Models/Product.cs
namespace CrudApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public required int CategoryId { get; set; }
        public string? Description { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category? Category { get; set; }
    }
}
