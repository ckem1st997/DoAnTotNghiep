using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire
{
    public class GetClaims : IGetClaims
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public GetClaims(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Task<string> GetIdUserByClaims()
        {
            throw new NotImplementedException();
        }
     
        public string GetUserNameByClaims()
        {
            var identity = _contextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                return identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            }
            return string.Empty;
        }
    }
}
