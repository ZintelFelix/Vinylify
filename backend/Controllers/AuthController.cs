using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vinylify.Backend.Models;
using Vinylify.Backend.Services;

namespace Vinylify.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IDiscogsAuthService _authService;
        private readonly DiscogsSettings    _settings;

        public AuthController(
            IDiscogsAuthService authService,
            IOptions<DiscogsSettings> options)
        {
            _authService = authService;
            _settings    = options.Value;
        }

        /// GET  /api/auth/request-token
        [HttpGet("request-token")]
        public async Task<IActionResult> GetRequestToken()
        {
            var raw = await _authService.GetRequestTokenAsync();
            var parts = raw
                .Split('&', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Split('=', 2))
                .ToDictionary(kv => kv[0], kv => kv[1]);

            if (!parts.TryGetValue("oauth_token", out var token) ||
                !parts.TryGetValue("oauth_token_secret", out var secret))
            {
                return StatusCode(502, "Ungültige Antwort von Discogs");
            }

            var authorizeUrl =
                $"{_settings.AuthorizeUrl}" +
                $"?oauth_token={Uri.EscapeDataString(token)}" +
                $"&oauth_callback={Uri.EscapeDataString(_settings.CallbackUrl)}";

            return Ok(new
            {
                oauthToken        = token,
                oauthTokenSecret  = secret,
                authorizeUrl
            });
        }

        /// POST /api/auth/access-token
        [HttpPost("access-token")]
        public async Task<IActionResult> GetAccessToken([FromBody] AccessTokenRequest req)
        {
            if (string.IsNullOrEmpty(req.OAuthToken) ||
                string.IsNullOrEmpty(req.OAuthTokenSecret) ||
                string.IsNullOrEmpty(req.OAuthVerifier))
            {
                return BadRequest("Ungültige Access-Token-Anfrage.");
            }

            var result = await _authService.GetAccessTokenAsync(new AccessTokenRequest
            {
                OAuthToken       = req.OAuthToken,
                OAuthTokenSecret = req.OAuthTokenSecret,
                OAuthVerifier    = req.OAuthVerifier
            });

            // result enthält OAuthToken, OAuthTokenSecret, UserName
            return Ok(new
            {
                oauthToken       = result.OAuthToken,
                oauthTokenSecret = result.OAuthTokenSecret,
                userName         = result.UserName
            });
        }
    }
}