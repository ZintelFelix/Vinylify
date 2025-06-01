using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public class DiscogsSearchService : IDiscogsSearchService
    {
        private readonly DiscogsSettings _settings;

        public DiscogsSearchService(IOptions<DiscogsSettings> opts)
        {
            _settings = opts.Value;
        }

        public async Task<SearchResultDto> SearchAsync(string query, string oauthToken, string oauthSecret, int page = 1)
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

            var request = new RestRequest("/database/search", Method.Get);
            request.AddQueryParameter("q", query);
            request.AddQueryParameter("page", page.ToString());
            request.AddQueryParameter("per_page", "20");

            var resp = await client.ExecuteAsync<SearchResultDto>(request);
            if (!resp.IsSuccessful || resp.Data == null)
                throw new HttpRequestException($"Discogs-Suche fehlgeschlagen: {resp.StatusCode}");

            return resp.Data;
        }
    }
}
