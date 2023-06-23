using IAsyncEnumerableExample.Web.Models;
using IAsyncEnumerableExample.Web.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IAsyncEnumerableExample.Web.Controllers
{
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IProductRepository rep)
        {
            _rep = rep;
        }

        private readonly IProductRepository _rep;

        [HttpGet]
        public IAsyncEnumerable<Product> GetAsyncStream(bool onlyEnabled, CancellationToken ct)
        {
            this.DisableBuffering();
            return _rep.GetEnumerableListAsync(onlyEnabled, ct);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetList(bool onlyEnabled, CancellationToken ct)
        {
            var retval = await _rep.GetClassicListAsync(onlyEnabled, ct);
            return Ok(retval);
        }
    }
}
