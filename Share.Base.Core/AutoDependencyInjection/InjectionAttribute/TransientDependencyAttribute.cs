using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Base.Core.AutoDependencyInjection.InjectionAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class TransientDependencyAttribute : Attribute
    {
        public TransientDependencyAttribute()
        {
        }
    }
}
