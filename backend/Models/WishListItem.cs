namespace Vinylify.Backend.Models
{
    public class WishListItem
    {
        // Composite Key: UserId + DiscogsId (siehe DbContext)
        public string UserId    { get; set; } = string.Empty;
        public int    DiscogsId { get; set; }
        public string Title     { get; set; } = string.Empty;
        public string Artist    { get; set; } = string.Empty;
        public string ThumbUrl  { get; set; } = string.Empty;
    }
}
