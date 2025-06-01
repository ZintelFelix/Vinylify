using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public class DiscogsReleaseService : IDiscogsReleaseService
    {
        private readonly DiscogsSettings _settings;

        public DiscogsReleaseService(IOptions<DiscogsSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<ReleaseDetailDto> GetReleaseAsync(int releaseId, string oauthToken, string oauthSecret)
        {
            var client = new RestClient(new RestClientOptions("https://api.discogs.com")
            {
                Authenticator = OAuth1Authenticator.ForProtectedResource(
                    _settings.ConsumerKey,
                    _settings.ConsumerSecret,
                    oauthToken,
                    oauthSecret
                )
            });

            var request = new RestRequest($"/releases/{releaseId}", Method.Get);
            var response = await client.ExecuteAsync<ReleaseDetailDto>(request);

            if (!response.IsSuccessful || response.Data == null)
                throw new HttpRequestException($"Release-Details fehlgeschlagen: {response.StatusCode}");

            return response.Data;
        }
    }
}
