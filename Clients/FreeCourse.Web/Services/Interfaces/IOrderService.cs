using FreeCourse.Web.Models.OrderServiceModels;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Senkron iletisim - Direkt order mikroservisine istek yapilacak
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);

        /// <summary>
        /// Asenkron iletisim - Siparis bilgileri RabbitMQ' ya gonderilecek
        /// </summary>
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task<List<OrderViewModel>> GetOrders();
    }
}