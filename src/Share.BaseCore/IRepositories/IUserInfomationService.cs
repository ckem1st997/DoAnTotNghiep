using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.IRepositories
{
    public interface IUserInfomationService
    {
        /// <summary>
        /// check from cache: key: iduser, key: listKey
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public Task<bool> GetAuthozireByUserIdToKey(string idUser, string Key);
        public Task<bool> GetAuthozireByUserIdToAuthorizeRole(string idUser, string authRole);

        public void GetInfoUserByClaims();
    }
}
