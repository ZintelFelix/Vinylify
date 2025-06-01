namespace Vinylify.Backend.Models
{
    public class DiscogsSettings
    {
        public string ConsumerKey     { get; set; } = default!;
        public string ConsumerSecret  { get; set; } = default!;
        public string RequestTokenUrl { get; set; } = default!;
        public string AuthorizeUrl    { get; set; } = default!;
        public string AccessTokenUrl  { get; set; } = default!;
        public string CallbackUrl     { get; set; } = default!;
    }
}
