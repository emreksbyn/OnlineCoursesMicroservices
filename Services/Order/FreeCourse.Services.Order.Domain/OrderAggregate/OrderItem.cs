using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public string? ProductId { get; private set; }
        public string? ProductName { get; private set; }
        public string? PictureUrl { get; private set; }
        public decimal Price { get; private set; }

        // Shallow Property
        // EF Core database de OrderId diye bir kolon bulunduracaktir fakat burada property olarak tanimlamamiz gerekmez.

        public OrderItem()
        {

        }
        public OrderItem(string? productId, string? productName, string? pictureUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        public void UpdateOrderItem(string productName, string pictureUrl, decimal price)
        {
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }
    }
}