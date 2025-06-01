using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinylify.Backend.Models;
using Vinylify.Backend.Services;

namespace Vinylify.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _svc;

        public WishListController(IWishListService svc)
        {
            _svc = svc;
        }

        /// <summary>
        /// GET /api/wishlist
        /// Header: X-Auth-UserName (erforderlich)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get([FromHeader(Name = "X-Auth-UserName")] string user)
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Missing user");

            var items = await _svc.GetWishlistAsync(user);
            return Ok(items);
        }

        /// <summary>
        /// POST /api/wishlist/add
        /// Header: X-Auth-UserName (erforderlich)
        /// Body: JSON mit discogsId, title, artist, thumbUrl (userId wird aus Header gefüllt)
        /// </summary>
        [HttpPost("add")]
        public async Task<IActionResult> Add(
            [FromHeader(Name = "X-Auth-UserName")] string user,
            [FromBody] WishListItem item
        )
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Missing user");

            // Sichere Stelle: überschreibe UserId mit Header
            item.UserId = user;
            await _svc.AddToWishlistAsync(user, item);
            return NoContent();
        }

        /// <summary>
        /// DELETE /api/wishlist/remove/{discogsId}
        /// Header: X-Auth-UserName (erforderlich)
        /// </summary>
        [HttpDelete("remove/{discogsId}")]
        public async Task<IActionResult> Remove(
            [FromHeader(Name = "X-Auth-UserName")] string user,
            int discogsId
        )
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Missing user");

            await _svc.RemoveFromWishlistAsync(user, discogsId);
            return NoContent();
        }
    }
}
