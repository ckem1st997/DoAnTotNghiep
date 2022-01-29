using System.Collections.Generic;

namespace WareHouse.API.Application.Queries.BaseModel
{
    public class WareHousesTreeModel : FancytreeItem
    {
        public WareHousesTreeModel()
        {
            children = new List<WareHousesTreeModel>();
        }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ParentId { get; set; }

        public string Path { get; set; }

        public string Description { get; set; }

        public bool Inactive { get; set; }


        public new IList<WareHousesTreeModel> children { get; set; }
    }
}