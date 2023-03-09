using FreeCourse.Services.FakePayment.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.FreeCourse.Shared.ControllerBases;
using Shared.FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            // paymnetDto ile odeme islemi gerceklestir.
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }
    }
}