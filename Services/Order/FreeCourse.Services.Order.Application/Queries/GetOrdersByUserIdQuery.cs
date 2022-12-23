using FreeCourse.Services.Order.Application.Dtos;
using MediatR;
using Shared.FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Order.Application.Queries
{
    //                                    Response olarak -> Response<List<OrderDto>>
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}