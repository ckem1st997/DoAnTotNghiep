using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WareHouse.API.Application.Queries.BaseModel
{
    public class BaseSearchModel
    {

        [DataMember]
        public string KeySearch { get; set; }

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