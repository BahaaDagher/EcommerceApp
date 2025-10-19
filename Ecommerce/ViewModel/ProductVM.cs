namespace Ecommerce.ViewModel
{
    public class ProductVM
    {

        public ICollection<Category> Categories { get; set; }
        public ICollection<Brand> Brands { get; set; }
        public Product? Product { get; set; } 
    }
}
