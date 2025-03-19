using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace EnterNow.Web
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private string _token;

        public void SetToken(string token)
        {
            _token = token;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (!string.IsNullOrEmpty(_token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(_token);
                identity = new ClaimsIdentity(jwt.Claims, "jwt");
            }
            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
