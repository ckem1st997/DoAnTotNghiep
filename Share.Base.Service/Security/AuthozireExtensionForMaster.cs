
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Security
{
    public class AuthozireExtensionForMaster : IAuthozireExtensionForMaster
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthozireExtensionForMaster(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool CheckUserIsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string GenerateJWT(IList<Claim> claims, int time)
        {
            if (claims is null)
            {
                throw new ArgumentNullException(nameof(claims));
            }
            claims.Add(new Claim("IpAddress", _contextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString()));
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthozireStringHelper.JWT.Secret));

            var token = new JwtSecurityToken(
                issuer: AuthozireStringHelper.JWT.ValidIssuer,
                audience: AuthozireStringHelper.JWT.ValidAudience,
                expires: DateTime.Now.AddYears(time),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GetClaimType(string type)
        {
            if (_contextAccessor.HttpContext.User.Identity is ClaimsIdentity identity && CheckUserIsAuthenticated())
            {
                IEnumerable<Claim> claims = identity.Claims;
                return identity.Claims.FirstOrDefault(c => c.Type.Equals(type))?.Value;
            }
            return string.Empty;
        }
    }
}

