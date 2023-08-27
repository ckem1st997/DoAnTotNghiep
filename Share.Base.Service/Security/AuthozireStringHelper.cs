using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Security
{
    public static class AuthozireStringHelper
    {
        public static class JWT
        {
            public const string Secret = "YAKKGEZQpB22rVx6M27yVkH0ESNPJldF";
            public const string ValidIssuer = "http://localhost:1997";
            public const string ValidAudience = "http://localhost:1997";
        }
    }
}
