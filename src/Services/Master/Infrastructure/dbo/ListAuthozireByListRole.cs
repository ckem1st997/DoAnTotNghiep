using Share.BaseCore.BaseNop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ListAuthozireByListRole : BaseEntity
    {
        public string AppId { get; set; }
        public string AuthozireId { get; set; }
        public string ListRoleId { get; set; }
    }
}