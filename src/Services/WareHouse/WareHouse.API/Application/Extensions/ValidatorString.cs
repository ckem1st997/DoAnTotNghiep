using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Extensions
{
    public static class ValidatorString
    {



        /// <summary>
        ///  Get Message to check null or empty
        /// </summary>
        /// <param name="Query">Message</param>
        /// <returns></returns>
        public static string GetMessageNotNull(string Query)
        {
            if(string.IsNullOrEmpty(Query))
                throw new ArgumentException("Parameter cannot be null", nameof(Query));
            return $"{Query} không thể bỏ trống !";
        }
    }
}
