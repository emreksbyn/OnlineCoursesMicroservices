using FreeCourse.Web.Models.DiscountServiceModels;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}