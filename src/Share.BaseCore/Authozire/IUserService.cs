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
    public interface IUserService<T> where T : BaseEntity
    {
        public Task<T> GetUser();
        public Task<bool> Register(string username, string password, string repassword);

    }
}
