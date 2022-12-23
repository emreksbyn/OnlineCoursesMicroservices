using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Mapping;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _dbContext;
        public GetOrdersByUserIdQueryHandler(OrderDbContext orderDbContext)
        {
            _dbContext = orderDbContext;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.BuyerId == request.UserId)
                .ToListAsync();

            if (!orders.Any()) return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(ordersDto, 200);
        }
    }
}