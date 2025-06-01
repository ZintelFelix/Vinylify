using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vinylify.Backend.Data;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public class WishListService : IWishListService
    {
        private readonly AppDbContext _db;

        public WishListService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Holt alle WishListItems für den angegebenen User.
        /// </summary>
        public async Task<IEnumerable<WishListItem>> GetWishlistAsync(string userId)
        {
            return await _db.WishListItems
                            .Where(w => w.UserId == userId)
                            .ToListAsync();
        }

        /// <summary>
        /// Fügt einen neuen WishListItem hinzu (UserId wird überschrieben).
        /// </summary>
        public async Task AddToWishlistAsync(string userId, WishListItem item)
        {
            item.UserId = userId;
            var exists = await _db.WishListItems.FindAsync(userId, item.DiscogsId);
            if (exists is null)
            {
                _db.WishListItems.Add(item);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Entfernt einen WishListItem anhand von UserId + DiscogsId.
        /// </summary>
        public async Task RemoveFromWishlistAsync(string userId, int discogsId)
        {
            var entity = await _db.WishListItems.FindAsync(userId, discogsId);
            if (entity != null)
            {
                _db.WishListItems.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
