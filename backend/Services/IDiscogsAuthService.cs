using System.Threading.Tasks;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public interface IDiscogsAuthService
    {
        /// <summary>
        /// Holt einen un-signierten Request-Token (oauth_token + oauth_token_secret).
        /// </summary>
        Task<string> GetRequestTokenAsync();

        /// <summary>
        /// Tauscht Request-Token + Verifier gegen Access-Token aus.
        /// </summary>
        Task<AccessTokenResponse> GetAccessTokenAsync(AccessTokenRequest request);
    }
}
