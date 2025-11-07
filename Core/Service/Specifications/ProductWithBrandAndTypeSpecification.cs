using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecification : BaseSpecification<Product,int>
    {
        public ProductWithBrandAndTypeSpecification(ProductQueryParams queryParams)
            :base(p=>(!queryParams.BrandId.HasValue || p.BrandId== queryParams.BrandId)
            &&(!queryParams.TypeId.HasValue||p.TypeId== queryParams.TypeId)
            &&(string.IsNullOrEmpty(queryParams.SearchValue)||p.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch(queryParams.sortingOption)
            {
                case ProductsSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductsSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductsSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductsSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);

        }

        public ProductWithBrandAndTypeSpecification(int id):base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
