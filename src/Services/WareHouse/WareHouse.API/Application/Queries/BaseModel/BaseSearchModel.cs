using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WareHouse.API.Application.Queries.BaseModel
{
    public class BaseSearchModel
    {
        
         [DataMember]
        public string KeySearch { get; set; }

        [DataMember] 
        public bool Active { get; set; } = true;
        
        
        [DataMember]
       // [Range(typeof(int), "1", "1000")]
        public int PageIndex { get; set; }


        [DataMember]
     //   [Range(typeof(int), "1", "1000")]
        public int PageNumber { get; set; }
    }
}