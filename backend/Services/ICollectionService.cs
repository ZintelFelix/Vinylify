using System.Collections.Generic;
using System.Threading.Tasks;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionItem>> GetCollectionAsync(string userId);
        Task AddToCollectionAsync(string userId, CollectionItem item);
        Task RemoveFromCollectionAsync(string userId, int discogsId);
    }
}
