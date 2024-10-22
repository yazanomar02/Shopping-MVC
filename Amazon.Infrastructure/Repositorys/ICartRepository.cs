using Amazon.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.Repositorys
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart CreateOrUpdate(Guid? cartId, Guid productId, int quantity = 1);
        Cart GetCartWithDetails(Guid? cartId);
    }
}
