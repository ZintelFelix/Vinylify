using System.Collections.Generic;
using System.Threading.Tasks;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public interface IWishListService
    {
        Task<IEnumerable<WishListItem>> GetWishlistAsync(string userId);
        Task AddToWishlistAsync(string userId, WishListItem item);
        Task RemoveFromWishlistAsync(string userId, int discogsId);
    }
}
