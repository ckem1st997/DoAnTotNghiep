using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Models
{
    public static class SelectListRole
    {


        public static IList<SelectListItem> Get()
        {
            var list = new List<SelectListItem>();
            var tem = new SelectListItem()
            {
                Text="User",
                Value="1"
            };
            list.Add(tem);
            var tem1 = new SelectListItem()
            {
                Text = "Manager",
                Value = "2"
            };
            list.Add(tem1);

            var tem2 = new SelectListItem()
            {
                Text = "Admin",
                Value = "3"
            };
            list.Add(tem2);

            return list;
        }
    }
}
