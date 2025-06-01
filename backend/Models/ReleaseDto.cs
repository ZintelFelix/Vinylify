namespace Vinylify.Backend.Models
{
    public class ReleaseDto
    {
        public int    Id       { get; set; }
        public string Title    { get; set; } = default!;
        public string Artist   { get; set; } = default!;
        public string ThumbUrl { get; set; } = default!;
    }
}
