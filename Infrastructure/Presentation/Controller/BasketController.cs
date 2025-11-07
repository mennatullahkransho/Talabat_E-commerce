using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjets.BasketModuleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string key)
        {
            var basket = await serviceManager.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateOrCreateBasket(CustomerBasketDto basket)
        {
            var updatedBasket = await serviceManager.BasketService.UpdateOrCreateBasketAsync(basket);
            return Ok(updatedBasket);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasket(string key)
        {
            var isDeleted = await serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(isDeleted);
        }
    }
}
