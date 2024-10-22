using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Domain.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }

        public Product()
        {
            ProductId = Guid.NewGuid();
        }
    }
}
