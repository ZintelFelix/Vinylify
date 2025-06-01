using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinylify.Backend.Models;
using Vinylify.Backend.Services;

namespace Vinylify.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IDiscogsSearchService _svc;

        public SearchController(IDiscogsSearchService svc)
            => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string q,
            [FromQuery] int page = 1,
            [FromHeader(Name = "X-Auth-Token")]  string token  = "",
            [FromHeader(Name = "X-Auth-Secret")] string secret = ""
        )
        {
            if (string.IsNullOrEmpty(q))
                return BadRequest("Query fehlt.");

            var result = await _svc.SearchAsync(q, token, secret, page);
            return Ok(result);
        }
    }
}
