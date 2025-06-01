using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinylify.Backend.Models;
using Vinylify.Backend.Services;

namespace Vinylify.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _svc;

        public CollectionController(ICollectionService svc)
        {
            _svc = svc;
        }

        /// <summary>
        /// GET  /api/collection
        /// Header: X-Auth-UserName
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromHeader(Name = "X-Auth-UserName")] string user)
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Missing user");

            var items = await _svc.GetCollectionAsync(user);
            return Ok(items);
        }

        /// <summary>
        /// POST /api/collection/add
        /// Header: X-Auth-UserName muss hier gesetzt sein
        /// Body: JSON mit discogsId, title, artist, thumbUrl, year (userId wird aus Header befüllt)
        /// </summary>
        [HttpPost("add")]
        public async Task<IActionResult> Add(
            [FromHeader(Name = "X-Auth-UserName")] string user,
            [FromBody] CollectionItem item
        )
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Missing user");

            // Überschreiben von UserId mit dem Header-Wert
            item.UserId = user;
            await _svc.AddToCollectionAsync(user, item);
            return NoContent();
        }

        /// <summary>
        /// DELETE /api/collection/remove/{discogsId}
        /// Header: X-Auth-UserName muss gesetzt sein
        /// </summary>
        [HttpDelete("remove/{discogsId}")]
        public async Task<IActionResult> Remove(
            [FromHeader(Name = "X-Auth-UserName")] string user,
            int discogsId
        )
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Missing user");

            await _svc.RemoveFromCollectionAsync(user, discogsId);
            return NoContent();
        }
    }
}
