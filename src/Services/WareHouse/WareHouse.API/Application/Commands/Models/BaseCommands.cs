using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Commands.Models
{
    public class BaseCommands
    {
        string _Id;
        public string Id
        {
            get { return _Id; }
            set { _Id = Guid.NewGuid().ToString(); }
        } 
    }
}
