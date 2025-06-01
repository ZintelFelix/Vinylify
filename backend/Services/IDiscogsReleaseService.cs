using System.Threading.Tasks;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    /// <summary>
    /// Kapselt den Abruf von Release-Details via Discogs-API.
    /// </summary>
    public interface IDiscogsReleaseService
    {
        /// <param name="releaseId">Discogs-Release-ID</param>
        /// <param name="oauthToken">Access-Token des Users</param>
        /// <param name="oauthSecret">Access-Token-Secret des Users</param>
        Task<ReleaseDetailDto> GetReleaseAsync(int releaseId, string oauthToken, string oauthSecret);
    }
}
