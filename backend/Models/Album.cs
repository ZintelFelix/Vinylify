namespace Vinylify.Backend.Models
{
    public class Album
    {
        public int    Id            { get; set; }            // PK
        public string DiscogsId     { get; set; } = default!; // z.B. "1234567"
        public string Title         { get; set; } = default!;
        public string Artist        { get; set; } = default!;
        public int    Year          { get; set; }
        public string Label         { get; set; } = default!;
        public string CoverImageUrl { get; set; } = default!;

        public ICollection<UserCollection> Collections { get; set; } = new List<UserCollection>();
    }
}
