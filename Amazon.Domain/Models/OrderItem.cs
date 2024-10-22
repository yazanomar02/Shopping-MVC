using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Domain.Models
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public required int Quantity { get; set; } // عدد القطع
        public virtual Product? Product { get; set; }
        public required Guid ProductId { get; set;}

    }
}
