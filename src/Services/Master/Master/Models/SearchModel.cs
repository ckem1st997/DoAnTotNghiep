using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Master.Models
{
    public class SearchModel
    {
        [Range(1, 1000, ErrorMessage = "Số bản ghi lớn hơn 0 và nhỏ hơn 1000 !")]

        public int Number { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số trang phải lớn hơn hoặc bằng 0 !")]

        public int Pages { get; set; }

        public string Key { get; set; }
        public string Id { get; set; }
    }
}
