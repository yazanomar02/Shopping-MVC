using Amazon.Domain.Models;
using Amazon.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.Repositorys
{
    public class CustomerRepository : GenericRepository<Customer>
    {
        public CustomerRepository(AmazonDbContext context) : base(context)
        {
        }
    }
}
