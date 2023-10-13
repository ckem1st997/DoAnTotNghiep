using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Attribute
{
    internal class KeyNameRoleAttribute : System.Attribute
    {
        public string Name { get; }
        public bool Block { get; }
        public KeyNameRoleAttribute(string name, bool block = false)
        {
            Name = name;
            Block = block;
        }
    }
}