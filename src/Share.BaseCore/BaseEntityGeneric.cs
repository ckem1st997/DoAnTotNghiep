using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore
{
    public class BaseEntityGeneric<T>
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [Key]
        public T Id { get; set; }
    }
}
