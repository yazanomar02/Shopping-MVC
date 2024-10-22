using Amazon.Domain.Models;
using Amazon.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.Repositorys
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(AmazonDbContext context) : base(context)
        {
        }
    }
}
