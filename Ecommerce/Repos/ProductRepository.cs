namespace Ecommerce.Repos
{
    public class ProductRepository :Repository<Product>
    {
        ApplicationDbContext _context = new ApplicationDbContext();

        public async Task  AddRange(IEnumerable<Product> products ,CancellationToken cancellationToken = default)
        {
            await _context.Products.AddRangeAsync(products , cancellationToken); 
        }
    }
}
