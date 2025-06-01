using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using Vinylify.Backend.Models;

namespace Vinylify.Backend.Services
{
    public class DiscogsAuthService : IDiscogsAuthService
    {
        private readonly DiscogsSettings _settings;

        public DiscogsAuthService(IOptions<DiscogsSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<string> GetRequestTokenAsync()
        {
            var client = new RestClient(new RestClientOptions(_settings.RequestTokenUrl)
            {
                Authenticator = OAuth1Authenticator.ForRequestToken(
                    _settings.ConsumerKey,
                    _settings.ConsumerSecret,
                    callbackUrl: _settings.CallbackUrl ?? "oob"
                )
            });

            var request = new RestRequest(string.Empty, Method.Post);
            var resp = await client.ExecuteAsync(request);
            if (!resp.IsSuccessful)
                throw new HttpRequestException($"Request-Token fehlgeschlagen: {resp.StatusCode} – {resp.Content}");

            return resp.Content!;
        }

        public async Task<AccessTokenResponse> GetAccessTokenAsync(AccessTokenRequest req)
        {
            // 1) Erzeuge Client für Access-Token
            var client = new RestClient(new RestClientOptions(_settings.AccessTokenUrl)
            {
                Authenticator = OAuth1Authenticator.ForAccessToken(
                    _settings.ConsumerKey,
                    _settings.ConsumerSecret,
                    req.OAuthToken,
                    req.OAuthTokenSecret,
                    req.OAuthVerifier
                )
            });

            // 2) Führe POST ohne Body aus
            var request = new RestRequest(string.Empty, Method.Post);
            var resp = await client.ExecuteAsync(request);
            if (!resp.IsSuccessful)
                throw new HttpRequestException($"Access-Token fehlgeschlagen: {resp.StatusCode} – {resp.Content}");

            // 3) Parse die Antwort: "oauth_token=…&oauth_token_secret=…&username=…"
            var parts = resp.Content!
                            .Split('&', StringSplitOptions.RemoveEmptyEntries)
                            .Select(p => p.Split('=', 2))
                            .ToDictionary(kv => kv[0], kv => kv[1]);

            return new AccessTokenResponse
            {
                OAuthToken       = parts["oauth_token"],
                OAuthTokenSecret = parts["oauth_token_secret"],
                UserName         = parts.ContainsKey("username") ? parts["username"] : string.Empty
            };
        }
    }
}
