using FreeCourse.Services.Discount.Services;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.FreeCourse.Shared.ControllerBases;
using Shared.FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _identityService;
        public DiscountsController(IDiscountService discountService, ISharedIdentityService identityService)
        {
            _discountService = discountService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }

        // api/Discounts/67
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Response<Models.Discount> discount = await _discountService.GetById(id);
            return CreateActionResultInstance(discount);
        }

        [HttpGet("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            string userId = _identityService.GetUserId;
            Response<Models.Discount> discount = await _discountService.GetByCodeAndUserId(code, userId);
            return CreateActionResultInstance(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Save(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Update(discount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.Delete(id));
        }
    }
}