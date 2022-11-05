using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire
{
    public interface IGetClaims
    {
        public string GetIdUserByClaims();
        public string GetIpAddressClaims();
        public string GetUserNameByClaims();
      
    }

   
}
