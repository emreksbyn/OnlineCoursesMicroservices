namespace FreeCourse.Web.Models.BasketServiceModels
{
    public class BasketItemViewModel
    {
        public string? CourseId { get; set; }
        public string? CourseName { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
        private decimal? DiscountAppliedPrice { get; set; }
        public void AppliedDiscount(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }
        public decimal GetCurrentPrice
        {
            get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price;
        }
    }
}