namespace Vinylify.Backend.Models
{
    /// <summary>
    /// Enthält den finalen Access-Token und das Secret
    /// </summary>
    public class AccessTokenResponse
    {
        public string OAuthToken { get; set; } = default!;
        public string OAuthTokenSecret { get; set; } = default!;
        public string UserName { get; set; } = default!;    // optional, falls Discogs-Username zurückkommt
    }
}
