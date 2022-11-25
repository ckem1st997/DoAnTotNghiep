using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire
{
    /// <summary>
    /// using in service master manager authozire
    /// </summary>
    public partial interface IAuthozireExtensionForMaster
    {
        /// <summary>
        /// implemention in service master manager authozire : GenerateJWT
        /// </summary>
        public string GenerateJWT(IList<Claim> claims, int time);
        // public Task<string> GenerateRefreshToken();
        public string GetClaimType(string type);
        public bool CheckUserIsAuthenticated();
    }
}
