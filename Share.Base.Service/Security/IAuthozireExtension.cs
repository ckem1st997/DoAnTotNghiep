using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Security
{
    /// <summary>
    /// using in service master manager authozire
    /// </summary>
    public partial interface IAuthorizeExtension
    {
        /// <summary>
        /// implemention in service master manager authozire : GenerateJWT
        /// </summary>
        public string GenerateJWT(IList<Claim> claims, int time);
        // public Task<string> GenerateRefreshToken();
        bool IsAuthenticated { get; }

        string UserName { get; }
        string Token { get; }
        string AcessToken { get; }

        string ClaimType(string type);

        string RoleHealCheck { get; }
    }
}
