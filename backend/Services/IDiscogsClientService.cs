using System.Collections.Generic;
using System.Threading.Tasks;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public interface IDiscogsClientService
    {
        /// <summary>
        /// Ruft die ersten 50 Releases aus der Collection des authentifizierten Users ab.
        /// </summary>
        Task<IEnumerable<ReleaseDto>> GetUserCollectionAsync(
            string oauthToken,
            string oauthTokenSecret,
            string userName     // neu: Username als Parameter
        );
    }
}
