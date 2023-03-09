using FreeCourse.Web.Models.FakePaymentServiceModels;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}