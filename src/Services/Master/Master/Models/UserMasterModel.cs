using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Master.Models
{
    public class UserMasterModel : BaseModel
    {
        [Required(ErrorMessage = "Xin vui lòng nhập Email !"), MaxLength(50), DataType(DataType.EmailAddress), RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool InActive { get; set; }
        public string Role { get; set; }
        public int RoleNumber { get; set; }
        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public string WarehouseId { get; set; }
        public string ListWarehouseId { get; set; }
        public IList<SelectListItem> RoleSelect { get; set; }
    }
}