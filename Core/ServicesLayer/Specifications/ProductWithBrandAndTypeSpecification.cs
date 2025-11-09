using DomainLayer.Models.Product;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product, int>
    {


        // Constructor for Get All
        public ProductWithBrandAndTypeSpecification(ProductQueryParams productQuery) 
            : base(p=>(!productQuery.brandId.HasValue || p.BrandId == productQuery.brandId)
                   &&(!productQuery.typeId.HasValue || p.TypeId == productQuery.typeId)
                   &&(string.IsNullOrWhiteSpace(productQuery.searchvalue) || p.Name.ToLower().Contains(productQuery.searchvalue.ToLower())))
        //p => p.BrandId == brandId && p.TypeId == typeId false case 
        {
            // Includes
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            // Pagination
            ApplyPagination(productQuery.PageSize, productQuery.PageIndex);

            // Sorting
            switch (productQuery.sortingOptions) {
                case ProductSortingOptions.nameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.nameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.priceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.priceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    break;

            }
        }


        // Constructor for Get product By Id
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        
    }
}
