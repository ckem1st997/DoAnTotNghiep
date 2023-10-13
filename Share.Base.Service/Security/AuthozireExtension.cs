
using HotChocolate;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Share.Base.Core.AutoDependencyInjection.InjectionAttribute;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Security
{
    [ScopedDependency]
    public class AuthorizeExtension : IAuthorizeExtension
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthorizeExtension(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool IsAuthenticated
        {
            get
            {
                if (_contextAccessor == null || _contextAccessor.HttpContext == null ||
                    _contextAccessor.HttpContext.User?.Identity == null)
                {
                    throw new UnauthorizedAccessException("Unauthorized");
                }

                return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
        }

        public string UserName
        {
            get
            {
                string username = GetClaimType("username");
                if (string.IsNullOrEmpty(username))
                {
                    throw new UnauthorizedAccessException("Unauthorized");
                }
                return username;
            }
        }
        public string GenerateJWT(IList<Claim> claims, int time)
        {
            if (claims is null)
            {
                throw new ArgumentNullException(nameof(claims));
            }
            if (_contextAccessor.HttpContext is null)
                throw new ArgumentNullException(nameof(_contextAccessor.HttpContext));
            if (_contextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                claims.Add(new Claim("IpAddress", _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()));
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
        public string Token => GetToken();
        public string AcessToken => GetAcessToken();

        public string RoleHealCheck => "This is API to check Authorize !";

        public string ClaimType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException($"'{nameof(type)}' cannot be null or empty.", nameof(type));
            }
            return GetClaimType(type);
        }

        private string GetClaimType(string type)
        {
            if (_contextAccessor.HttpContext != null && _contextAccessor.HttpContext.User.Identity is ClaimsIdentity identity && IsAuthenticated)
            {
                var claims = identity.Claims.FirstOrDefault(c => c.Type.Equals(type));
                return claims?.Value ?? "";
            }
            throw new UnauthorizedAccessException("Unauthorized");
        }

        private string GetToken()
        {
            // Kiểm tra xem tiêu đề "Authorization" có tồn tại trong yêu cầu
            if (_contextAccessor.HttpContext != null && _contextAccessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                // Lấy giá trị của tiêu đề "Authorization" (chứa token)
                var authorizationHeader = _contextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

                // Token thường có định dạng "Bearer <token_value>", vì vậy bạn cần tách nó
                if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                {
                    return authorizationHeader;
                }
            }
            throw new UnauthorizedAccessException("Unauthorized");
        }

        private string GetAcessToken()
        {
            string[] tokens = GetToken().Split(" ");
            if (tokens.Length != 2)
            {
                throw new UnauthorizedAccessException("Unauthorized");
            }
            return tokens[1];
        }
    }
}

