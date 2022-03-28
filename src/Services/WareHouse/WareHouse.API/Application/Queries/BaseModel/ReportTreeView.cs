using System.Collections.Generic;

namespace WareHouse.API.Application.Queries.BaseModel
{
    public class ReportTreeView : FancytreeItem
    {
        public ReportTreeView()
        {
            children = new List<ReportTreeView>();
        }

        public string Code { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public string Path { get; set; }

        public new IList<ReportTreeView> children { get; set; }
    }
}