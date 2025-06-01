namespace Vinylify.Backend.Models
{
    public class UserCollection
    {
        public string UserId  { get; set; } = default!;  // z.B. Discogs-Username oder eigene User-ID
        public int    AlbumId { get; set; }

        public Album  Album   { get; set; } = null!;
    }
}
