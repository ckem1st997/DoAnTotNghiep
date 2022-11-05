using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire
{
    /// <summary>
    /// using in service master manager authozire
    /// </summary>
    public partial interface IAuthozireExtensionForMaster
    {
        public Task<string> GenerateJWT(string username, string password, bool remember=true);
    }
}
