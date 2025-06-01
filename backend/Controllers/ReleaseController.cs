using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinylify.Backend.Services;

namespace Vinylify.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReleaseController : ControllerBase
    {
        private readonly IDiscogsReleaseService _releaseService;

        public ReleaseController(IDiscogsReleaseService releaseService)
        {
            _releaseService = releaseService;
        }

        /// <summary>
        /// Liefert detaillierte Informationen zu einem Release.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRelease(
            int id,
            [FromHeader(Name = "X-Auth-Token")] string token,
            [FromHeader(Name = "X-Auth-Secret")] string secret
        )
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(secret))
                return BadRequest("Missing credentials");

            var detail = await _releaseService.GetReleaseAsync(id, token, secret);
            return Ok(detail);
        }
    }
}
