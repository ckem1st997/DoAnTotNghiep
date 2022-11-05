using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire
{
    /// <summary>
    /// implemention in service master
    /// </summary>
    public interface IAuthenForMaster
    {
        /// <summary>
        /// implemention in service master use method login
        /// </summary>
        public Task<bool> Login(string username, string password);
    }
}
