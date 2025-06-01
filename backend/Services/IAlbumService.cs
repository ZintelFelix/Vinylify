using System.Threading.Tasks;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    /// <summary>
    /// Kapselt das lokale Caching und die Verwaltung
    /// der Discogs-Releases in der eigenen DB.
    /// </summary>
    public interface IAlbumService
    {
        /// <summary>
        /// Legt einen Album-Eintrag an, falls noch nicht vorhanden.
        /// </summary>
        Task<Album> EnsureAlbumAsync(ReleaseDto dto);

        /// <summary>
        /// Fügt ein Release (by Discogs-ID) in die User-Sammlung ein.
        /// </summary>
        Task<bool> AddToCollectionAsync(string userId, string discogsReleaseId);

        /// <summary>
        /// Entfernt ein Release (by Discogs-ID) aus der User-Sammlung.
        /// </summary>
        Task<bool> RemoveFromCollectionAsync(string userId, string discogsReleaseId);
    }
}
