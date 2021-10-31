using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.Domain.Exceptions
{
    public partial class WareHouseDomainException : Exception
    {
        public WareHouseDomainException()
        { }

        public WareHouseDomainException(string message)
            : base(message)
        { }

        public WareHouseDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}