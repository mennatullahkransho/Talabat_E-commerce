using DomainLayer.Models;
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
        public ProductWithBrandAndTypeSpecification(int? BrandId, int? TypeId, ProductsSortingOptions sortingOption)
            :base(p=>(!BrandId.HasValue|| p.BrandId==BrandId)&&(!TypeId.HasValue||p.TypeId==TypeId))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch(sortingOption)
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
        }

        public ProductWithBrandAndTypeSpecification(int id):base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
