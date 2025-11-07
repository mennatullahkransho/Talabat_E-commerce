using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjets.BasketModuleDto
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public ICollection<BasketItemDto> Items { get; set; } = [];
    }
}
