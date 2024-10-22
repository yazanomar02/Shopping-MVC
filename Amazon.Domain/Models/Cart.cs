using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Domain.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsOrdered { get; set; }
        public string? CustomerEmail { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set;}

        public Cart()
        {
            CartId = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            OrderItems = new List<OrderItem>();
        }
    }
}
