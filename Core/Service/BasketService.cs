using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Interfaces;
using DomainLayer.Models.BasketModule;
using ServiceAbstraction;
using Shared.DataTransferObjets.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string key) => await basketRepository.DeleteBasketAsync(key);

        public async Task<CustomerBasketDto> GetBasketAsync(string key)
        {
           var basket= await basketRepository.GetBasketAsync(key);
            if (basket is not null)
                return mapper.Map<CustomerBasket, CustomerBasketDto>(basket);
            else
                throw new BasketNotFoundException(key);
                
           
        }

        public async Task<CustomerBasketDto?> UpdateOrCreateBasketAsync(CustomerBasketDto basket)
        {
            var customerBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var CreatedORUpdatedBasket = await basketRepository.UpdateOrCreateBasketAsync(customerBasket);
            if (CreatedORUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Cannot create or update basket");
        }
    }
}
