using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Core.Infrastructure
{
    public class BaseSearchModel
    {

        [DataMember]
        public string? KeySearch { get; set; }

        [DataMember]
        public bool? Active { get; set; }


        [DataMember]
        // [Range(typeof(int), "1", "1000")]
        public int Skip { get; set; } = 0;


        [DataMember]
        //   [Range(typeof(int), "1", "1000")]
        public int Take { get; set; } = 1000;
    }
}
