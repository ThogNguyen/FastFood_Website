namespace Services.Models.ProductModel
{
    public class ProductForView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
