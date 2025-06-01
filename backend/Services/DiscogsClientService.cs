using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public class DiscogsClientService : IDiscogsClientService
    {
        private readonly DiscogsSettings _settings;

        public DiscogsClientService(IOptions<DiscogsSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<IEnumerable<ReleaseDto>> GetUserCollectionAsync(
            string oauthToken,
            string oauthTokenSecret,
            string userName
        )
        {
            var client = new RestClient(new RestClientOptions("https://api.discogs.com")
            {
                Authenticator = OAuth1Authenticator.ForProtectedResource(
                    _settings.ConsumerKey,
                    _settings.ConsumerSecret,
                    oauthToken,
                    oauthTokenSecret
                )
            });

            // Folder 0, max 50 pro Seite
            var request = new RestRequest($"/users/{Uri.EscapeDataString(userName)}/collection/folders/0/releases", Method.Get)
                .AddParameter("per_page", "50");

            var resp = await client.ExecuteAsync<DiscogsCollectionResponse>(request);
            if (!resp.IsSuccessful || resp.Data == null)
                throw new HttpRequestException($"Collection abholen fehlgeschlagen: {resp.StatusCode}");

            return resp.Data.Releases.Select(r => new ReleaseDto
            {
                Id       = r.Id,
                Title    = r.BasicInformation.Title,
                Artist   = string.Join(", ", r.BasicInformation.Artists.Select(a => a.Name)),
                ThumbUrl = r.BasicInformation.Thumb
            });
        }
    }
}
