using System.Text.Json.Serialization;

namespace Vinylify.Backend.Models
{
    /// <summary>
    /// DTO für den Body der Access-Token-Anfrage.
    /// </summary>
    public class AccessTokenRequest
    {
        [JsonPropertyName("oauthToken")]
        public string OAuthToken { get; set; } = default!;

        [JsonPropertyName("oauthTokenSecret")]
        public string OAuthTokenSecret { get; set; } = default!;

        [JsonPropertyName("oauthVerifier")]
        public string OAuthVerifier { get; set; } = default!;
    }
}
