using FreeCourse.Services.Basket.Dtos;
using Shared.FreeCourse.Shared.Dtos;
using StackExchange.Redis;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            bool status = await _redisService.GetDatabase().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            RedisValue existBasket = await _redisService.GetDatabase().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existBasket))
                return Response<BasketDto>.Fail("Basket not found", 404);

            BasketDto deserilizeBasket = JsonSerializer.Deserialize<BasketDto>(existBasket);
            return Response<BasketDto>.Success(deserilizeBasket, 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            string serilizeBasket = JsonSerializer.Serialize(basketDto);

            bool status = await _redisService.GetDatabase().StringSetAsync(basketDto.UserId, serilizeBasket);

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not update or save", 500);
        }
    }
}