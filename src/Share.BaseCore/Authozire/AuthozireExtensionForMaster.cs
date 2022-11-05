using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire
{
    public class AuthozireExtensionForMaster : IAuthozireExtensionForMaster
    {
        private readonly IAuthenForMaster _authenForMaster;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthozireExtensionForMaster(IAuthenForMaster authenForMaster, IHttpContextAccessor contextAccessor)
        {
            _authenForMaster = authenForMaster;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> GenerateJWT(string username, string password, bool remember = true)
        {
            if (username == null || password == null)
                throw new BaseException("username or password is null !");
            if (await _authenForMaster.Login(username, password))
            {
                List<Claim> authClaims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, username),
                                new Claim("IpAddress", _contextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString())
                            };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthozireStringHelper.JWT.Secret));

                var token = new JwtSecurityToken(
                    issuer: AuthozireStringHelper.JWT.ValidIssuer,
                    audience: AuthozireStringHelper.JWT.ValidAudience,
                    expires: remember ? DateTime.Now.AddYears(1) : DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
                return string.Empty;
        }

        public Task<bool> Register(string username, string password, string repassword)
        {
            throw new NotImplementedException();
        }
    }
}
