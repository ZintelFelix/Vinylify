using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vinylify.Backend.Data;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly AppDbContext _db;

        public AlbumService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Album> EnsureAlbumAsync(ReleaseDto dto)
        {
            // Prüfen, ob das Album (Discogs-Release) schon lokal existiert
            var album = await _db.Albums
                .FirstOrDefaultAsync(a => a.DiscogsId == dto.Id.ToString());

            if (album == null)
            {
                album = new Album
                {
                    DiscogsId     = dto.Id.ToString(),
                    Title         = dto.Title,
                    Artist        = dto.Artist,
                    CoverImageUrl = dto.ThumbUrl
                    // Year, Label etc. kannst Du bei Bedarf hier ergänzen
                };
                _db.Albums.Add(album);
                await _db.SaveChangesAsync();
            }

            return album;
        }

        public async Task<bool> AddToCollectionAsync(string userId, string discogsReleaseId)
        {
            // Album in localem Cache sicherstellen
            // (Voraussetzung: ReleaseDto ist bereits im lokalen DB-Kontext)
            var album = await _db.Albums
                .FirstOrDefaultAsync(a => a.DiscogsId == discogsReleaseId);

            if (album == null)
                return false;

            // Prüfen, ob der User-Collection-Eintrag schon existiert
            var exists = await _db.UserCollections
                .FindAsync(userId, album.Id);

            if (exists != null)
                return true;

            // Neuen Eintrag anlegen
            _db.UserCollections.Add(new UserCollection
            {
                UserId  = userId,
                AlbumId = album.Id
            });
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCollectionAsync(string userId, string discogsReleaseId)
        {
            // Album aus DB suchen
            var album = await _db.Albums
                .FirstOrDefaultAsync(a => a.DiscogsId == discogsReleaseId);

            if (album == null)
                return false;

            // UserCollection-Eintrag suchen
            var uc = await _db.UserCollections
                .FindAsync(userId, album.Id);

            if (uc == null)
                return false;

            _db.UserCollections.Remove(uc);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
