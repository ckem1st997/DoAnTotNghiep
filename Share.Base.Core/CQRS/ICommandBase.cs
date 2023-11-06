using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.CQRS
{
    public interface ICommandBase<T> : IRequest<T>
    {
    }
    public interface ICommandBase : IRequest
    {
    }
}
