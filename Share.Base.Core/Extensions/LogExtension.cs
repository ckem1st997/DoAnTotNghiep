using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.Extensions
{
    public static class LogExtension
    {

        public static void Information(string message)
        {
            Log.Information(message);
        }

        public static void Warning(string message)
        {
            Log.Warning(message);
        }


        public static void Error(string message)
        {
            Log.Error(message);
        }


        public static void Verbose(string message)
        {
            Log.Verbose(message);
        }
    }
}
