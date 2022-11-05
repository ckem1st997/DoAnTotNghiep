using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire
{
    /// <summary>
    /// implemention in service use method login
    /// </summary>
    public interface IAuthenForMaster
    {
        public Task<bool> Login(string username, string password);
    }
}
