using FreeCourse.Web.Handler;
using FreeCourse.Web.Services.Interfaces;
using FreeCourse.Web.Services;
using FreeCourse.Web.Models;

namespace FreeCourse.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void AddHttpClientServices(this IServiceCollection service, IConfiguration configuration)
        {
            ServiceApiSettings serviceApiSettings = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            service.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            }
            // UserService icinde bir client kullanildiginda git ResourceOwnerPasswordTokenHandler' dan JWT al ve header' ina ekle demis oluyoruz.
            ).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

            service.AddHttpClient<IIdentityService, IdentityService>();

            service.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
            }
            ).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            service.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}");
            }
            ).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            service.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Discount.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient<IPaymentService, PaymentService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Payment.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            service.AddHttpClient<IOrderService, OrderService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Order.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
        }
    }
}