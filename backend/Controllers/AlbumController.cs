using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinylify.Backend.Services;

namespace Vinylify.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albums;

        public AlbumController(IAlbumService albums)
            => _albums = albums;

        /// <summary>
        /// Fügt das angegebene Release in die Sammlung des Users ein.
        /// </summary>
        [HttpPost("add/{releaseId}")]
        public async Task<IActionResult> Add(
            string releaseId,
            [FromHeader(Name = "X-Auth-UserName")] string user
        )
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Kein User im Header.");

            var ok = await _albums.AddToCollectionAsync(user, releaseId);
            return ok ? NoContent() : NotFound();
        }

        /// <summary>
        /// Entfernt das angegebene Release aus der Sammlung des Users.
        /// </summary>
        [HttpDelete("remove/{releaseId}")]
        public async Task<IActionResult> Remove(
            string releaseId,
            [FromHeader(Name = "X-Auth-UserName")] string user
        )
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Kein User im Header.");

            var ok = await _albums.RemoveFromCollectionAsync(user, releaseId);
            return ok ? NoContent() : NotFound();
        }
    }
}
