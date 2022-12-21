using Dapper;
using Npgsql;
using Shared.FreeCourse.Shared.Dtos;
using System.Data;
using System.Data.SqlClient;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            //_dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
            _dbConnection = new SqlConnection(_configuration.GetConnectionString("MsSql"));
        }
        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            IEnumerable<Models.Discount> discounts = await _dbConnection.QueryAsync<Models.Discount>(
                "SELECT * FROM Discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            IEnumerable<Models.Discount> discounts = await _dbConnection.QueryAsync<Models.Discount>(
                "SELECT * FROM Discount WHERE id@Id",
                new
                {
                    Id = id
                });

            Models.Discount discount = discounts.SingleOrDefault();

            if (discount == null) return Response<Models.Discount>.Fail("Discount not found", 404);
            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            IEnumerable<Models.Discount> discounts = await _dbConnection.QueryAsync<Models.Discount>(
                 "SELECT * FROM Discount WHERE userid=@UserId AND code=@Code",
                 new
                 {
                     Code = code,
                     UserId = userId
                 });
            Models.Discount discount = discounts.FirstOrDefault();

            if (discount == null) return Response<Models.Discount>.Fail("Discount not found", 404);
            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            int saveStatus = await _dbConnection.ExecuteAsync(
                "INSERT INTO Discount (userid, rate, code) VALUES (@UserId, @Rate, @Code)",
                discount
                );
            if (saveStatus > 0) return Response<NoContent>.Success(204);
            return Response<NoContent>.Fail("An error occurred while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            int updateStatus = await _dbConnection.ExecuteAsync(
                "UPDATE Discount SET userid=@UserId, code=@Code, rate=@Rate WHERE id=@Id",
                new
                {
                    Id = discount.Id,
                    UserId = discount.UserId,
                    Code = discount.Code,
                    Rate = discount.Rate
                });

            if (updateStatus > 0) return Response<NoContent>.Success(204);
            return Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            int deleteStatus = await _dbConnection.ExecuteAsync(
                "DELETE FROM Discount WHERE id=@Id",
                new { Id = id });
            return deleteStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}