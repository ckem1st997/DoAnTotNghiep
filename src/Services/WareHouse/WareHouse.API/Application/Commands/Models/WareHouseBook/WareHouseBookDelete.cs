using System.Collections.Generic;

namespace WareHouse.API.Application.Commands.Models
{
    public partial class WareHouseBookDelete
    {
        public IEnumerable<string> idsIn { get; set; }
        public IEnumerable<string> idsOut { get; set; }
    }
}
