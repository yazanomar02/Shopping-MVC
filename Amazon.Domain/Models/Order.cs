using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Domain.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set;}
        public Customer Customer { get; set; }
        public required Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal OrderTotal => OrderItems?.Sum(item => item.Product?.Price * item.Quantity) ?? 0;

        public Order()
        {
            OrderItems = new List<OrderItem>();
            CreatedAt = DateTime.UtcNow;
            OrderId = Guid.NewGuid();
        }
    }
}
