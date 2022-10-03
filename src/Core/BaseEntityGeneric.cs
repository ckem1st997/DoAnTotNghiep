using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BaseEntityGeneric<T>
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public T Id { get; set; }
    }
}
