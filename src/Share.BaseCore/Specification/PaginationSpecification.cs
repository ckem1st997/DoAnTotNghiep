using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Specification
{
    /// <summary>
    /// This object holds the pagination query specifications.
    /// </summary>
    /// <typeparam name="T">The database entity i.e an <see cref="DbSet{TEntity}"/> object.</typeparam>
    public class PaginationSpecification<T> : SpecificationBase<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the current page index.
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }
    }


    public class PaginatedList<T>
       where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// The constructor takes necessary info for pagiantion.
        /// </summary>
        /// <param name="items">The items of current page.</param>
        /// <param name="totalItems">Total item count of the list.</param>
        /// <param name="pageIndex">Current page index.</param>
        /// <param name="pageSize">Pagiantion page size.</param>
        public PaginatedList(List<T> items, long totalItems, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            TotalItems = totalItems;
            Items = new List<T>(pageSize);
            Items.AddRange(items);
        }

        // This is for serialization.
        private PaginatedList()
        {
        }

        /// <summary>
        /// Gets the index of the current page.
        /// </summary>
        public int PageIndex { get; }

        /// <summary>
        /// Gets the number of items in each page.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Gets total page count of the list.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Gets total items count in the list.
        /// </summary>
        public long TotalItems { get; }

        /// <summary>
        /// Gets the items of the current page.
        /// </summary>
        public List<T> Items { get; }
    }


}
