using OrderAPI.Repositories.Repon;

namespace OrderAPI.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
