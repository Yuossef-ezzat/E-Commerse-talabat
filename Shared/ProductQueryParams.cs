using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParams
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public ProductSortingOptions? sortingOptions { get; set; }
        public string? searchvalue { get; set; }

        #region pagination
        private const int maxPageSize = 10;
        private const int DefultPageSize = 5;
        public int PageIndex { get; set; } = 1;
        private int pageSize = DefultPageSize;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? DefultPageSize : value ; }
        }

        #endregion

    }
}
