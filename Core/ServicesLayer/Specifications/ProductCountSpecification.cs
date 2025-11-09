using DomainLayer.Models.Product;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Specifications
{
    public class ProductCountSpecification : BaseSpecifications<Product,int>
    {
        // Constructor for Count All
        public ProductCountSpecification(ProductQueryParams productQuery)
            : base(p => (!productQuery.brandId.HasValue || p.BrandId == productQuery.brandId)
                   && (!productQuery.typeId.HasValue || p.TypeId == productQuery.typeId)
                   && (string.IsNullOrWhiteSpace(productQuery.searchvalue) || p.Name.ToLower().Contains(productQuery.searchvalue.ToLower())))
        {

        }
    }
}
