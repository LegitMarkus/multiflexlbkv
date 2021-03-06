

namespace Frontend.Logic.Entities.Orders
{
    public class OrderItem : VersionEntity
    {
        public Product Product { get; set; } = new Product();
    }

    public class RootOrderItem : RootShippingAddress
    {
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
