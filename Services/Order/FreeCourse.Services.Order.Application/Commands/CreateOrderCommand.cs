using FreeCourse.Services.Order.Application.Dtos;
using MediatR;
using Shared.FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string? BuyerId { get; set; }
        public List<OrderItemDto>? OrderItems { get; set; }
        public AddressDto? Address { get; set; }
    }
}