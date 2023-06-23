using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace IAsyncEnumerableExample.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        protected String? CurrentLogin => this.HttpContext?.User?.Identity?.Name;

        protected void DisableBuffering()
        {
            HttpContext?.Features?.Get<IHttpResponseBodyFeature>()?.DisableBuffering();
        }
    }
}
