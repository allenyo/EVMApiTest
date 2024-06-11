using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace EVMAPI.Authentication
{
    public class CustomAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IHttpClientFactory httpClientFactory) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationToken = Request.Headers.Authorization;
            if (string.IsNullOrEmpty(authorizationToken))
            {
                return Task.FromResult(AuthenticateResult.Fail("No token provided."));
            }

            var httpClient = httpClientFactory.CreateClient();
            var response = /*await httpClient.GetAsync($"hc url ");*/ authorizationToken == "1234567";

            if (!response)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid token."));
            }

            var claims = new[] { new Claim(ClaimTypes.Name, "User") }; // get claims from hc response if its possible to authorize
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));        
        }
    }
}

