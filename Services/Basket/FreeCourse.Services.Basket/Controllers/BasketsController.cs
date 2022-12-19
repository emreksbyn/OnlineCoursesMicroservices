using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.FreeCourse.Shared.ControllerBases;
using Shared.FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _identityService;
        public BasketsController(IBasketService basketService, ISharedIdentityService identityService)
        {
            _basketService = basketService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            string userId = _identityService.GetUserId;
            Response<BasketDto> basketDto = await _basketService.GetBasket(userId);
            return CreateActionResultInstance(basketDto);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            Response<bool> response = await _basketService.SaveOrUpdate(basketDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            string userId = _identityService.GetUserId;
            Response<bool> response = await _basketService.Delete(userId);
            return CreateActionResultInstance(response);
        }
    }
}