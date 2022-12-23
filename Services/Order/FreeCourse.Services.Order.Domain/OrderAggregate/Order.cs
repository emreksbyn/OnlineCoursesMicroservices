using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; set; }

        // Owned Entity Types
        // Ayarladigimiz takdirde Ef Core Address' e ait property' leri Order tablosuna kolon olarak ekler. Address icin ayri bir tablo olusturmaz.
        public Address? Address { get; set; }
        
        public string? BuyerId { get; set; }

        // Backing Field -> Encapculation yaptik. EfCore bu field' i doldurur. Asagidaki property ile de okuyabilecek hale getiriyoruz.
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        
        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
        }

        
        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            bool existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                OrderItem newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(orderItem => orderItem.Price);
    }
}