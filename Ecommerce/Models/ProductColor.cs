namespace Ecommerce.Models
{
    public class ProductColor
    {
        public int ProductId { get; set; }
        public string Color { get; set; }
        public Product Product { get; set; }
    }
}
