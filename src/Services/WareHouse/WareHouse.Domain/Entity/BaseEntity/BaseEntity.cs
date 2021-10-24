using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.Domain.Entity
{
    public partial class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();


        public BaseEntity()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString();
        }
    }
}
