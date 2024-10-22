using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Domain.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string PostalCode { get; set; }
        public required string ShippingAddress { get; set; }

        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
    }
}
