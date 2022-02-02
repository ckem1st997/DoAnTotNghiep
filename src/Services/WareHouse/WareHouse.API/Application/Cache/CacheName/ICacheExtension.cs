using System.Collections.Generic;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Cache.CacheName
{
    public interface ICacheExtension
    {
       IEnumerable<string> GetAllNameKey();
       IEnumerable<string> GetAllNameKeyByContains(string contains);

       Task RemoveAllKeys();
       
       Task RemoveAllKeysBy(string contains);
    }
}