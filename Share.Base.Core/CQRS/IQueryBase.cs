﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.CQRS
{

    public interface IQueryBase<T> : IRequest<T>
    {
    }
    public interface IQueryBase : IRequest
    {
    }
}