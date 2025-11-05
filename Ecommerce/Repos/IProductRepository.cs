using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repos
{
    public interface IProductRepository : IRepository<Product>
    {
        Task AddRange(IEnumerable<Product> products, CancellationToken cancellationToken = default);
    }
}
