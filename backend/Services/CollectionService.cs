using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vinylify.Backend.Data;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly AppDbContext _db;

        public CollectionService(AppDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Holt alle CollectionItems für den gegebenen User.
        /// </summary>
        public async Task<IEnumerable<CollectionItem>> GetCollectionAsync(string userId)
        {
            return await _db.CollectionItems
                            .Where(c => c.UserId == userId)
                            .ToListAsync();
        }

        /// <summary>
        /// Fügt ein neues CollectionItem hinzu (UserId wird überschrieben).
        /// </summary>
        public async Task AddToCollectionAsync(string userId, CollectionItem item)
        {
            item.UserId = userId;
            var exists = await _db.CollectionItems.FindAsync(userId, item.DiscogsId);
            if (exists is null)
            {
                _db.CollectionItems.Add(item);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Entfernt ein CollectionItem anhand von UserId + DiscogsId.
        /// </summary>
        public async Task RemoveFromCollectionAsync(string userId, int discogsId)
        {
            var entity = await _db.CollectionItems.FindAsync(userId, discogsId);
            if (entity != null)
            {
                _db.CollectionItems.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }
    }
}
