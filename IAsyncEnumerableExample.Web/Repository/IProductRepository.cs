using IAsyncEnumerableExample.Web.Models;

namespace IAsyncEnumerableExample.Web.Repository;

public interface IProductRepository
{
    Task<IReadOnlyCollection<Product>> GetClassicListAsync(bool onlyEnabled, CancellationToken ct = default);
    IAsyncEnumerable<Product> GetEnumerableListAsync(bool onlyEnabled, CancellationToken ct = default);
}