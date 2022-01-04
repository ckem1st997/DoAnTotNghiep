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
        
        /// <summary>
        /// Get Message to check min
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetMessageToMin(int min)
        {
            return $"Giá trị không thể nhỏ hơn {min} !";
        }
        
        /// <summary>
        /// Get Message to check max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetMessageToMax(int max)
        {
            return $"Giá trị không thể lớn hơn {max} !";
        }
    }
}
