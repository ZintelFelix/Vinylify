namespace Vinylify.Backend.Models
{
    public class CollectionItem
    {
        public string UserId    { get; set; } = default!;
        public int    DiscogsId { get; set; }
        public string Title     { get; set; } = default!;
        public string Artist    { get; set; } = default!;
        public string ThumbUrl  { get; set; } = default!;
        public int    Year      { get; set; }
    }
}
