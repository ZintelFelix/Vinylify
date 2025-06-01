using System.Threading.Tasks;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    /// <summary>
    /// Sucht Releases bei Discogs.
    /// </summary>
    public interface IDiscogsSearchService
    {
        Task<SearchResultDto> SearchAsync(string query, string oauthToken, string oauthSecret, int page = 1);
    }
}
