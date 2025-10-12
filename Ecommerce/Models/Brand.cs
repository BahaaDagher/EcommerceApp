namespace Ecommerce.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Img { get; set; } = "defaultImg.png";
        public ICollection<Product> Products { get; set; }
    }
}
