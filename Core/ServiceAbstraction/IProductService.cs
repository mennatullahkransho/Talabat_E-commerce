using Shared;
using Shared.DataTransferObjets.ProductModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParams queryParams);
        Task<ProductDto> GetProductByIdAsync(int Id);
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<TypeDto>> GetTypesAsync();

    }
}
