using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafKa.Net
{
    public  interface IKafKaConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

     //   IModel CreateModel();
    }
}