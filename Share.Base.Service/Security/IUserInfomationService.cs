using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Security
{
    /// <summary>
    /// call get info width grpc in service khác service master
    /// service master then write one service check to call in class implement equal get info user at cache or database
    /// implement in service
    /// </summary>
    public interface IUserInfomationService
    {
        /// <summary>
        /// check from cache: key: iduser, key: listKey
        /// Get width or Grpc, form class implement 
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public Task<bool> GetAuthozireByUserId(string idUser, string authRole);

        public void GetInfoUserByClaims();

    }
}
