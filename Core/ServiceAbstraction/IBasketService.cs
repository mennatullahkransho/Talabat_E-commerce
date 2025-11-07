using Shared.DataTransferObjets.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string key);
        Task<CustomerBasketDto?> UpdateOrCreateBasketAsync(CustomerBasketDto basket);
        Task<bool> DeleteBasketAsync(string key);
    }
}
