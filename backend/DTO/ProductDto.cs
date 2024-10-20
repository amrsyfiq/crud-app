namespace CrudApp.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? CategoryName { get; set; } 
    }
}
