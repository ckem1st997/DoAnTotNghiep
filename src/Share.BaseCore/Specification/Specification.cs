using Share.BaseCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Specification
{
    public class Specification<T> : SpecificationBase<T>
     where T : class
    {
        /// <summary>
        /// Gets or sets the value of number of item you want to skip in the query.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Gets or sets the value of number of item you want to take in the query.
        /// </summary>
        public int? Take { get; set; }
    }

}
