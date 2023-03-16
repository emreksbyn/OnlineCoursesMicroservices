namespace FreeCourse.Web.Models.OrderServiceModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        // Odeme gecmisinde adres' e ihtiyac yok
        // public AddressDto? Address { get; set; }
        public string? BuyerId { get; set; }
        public List<OrderItemViewModel>? OrderItems { get; set; }
    }
}