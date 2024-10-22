using Amazon.Domain.Models;
using Amazon.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.Repositorys
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AmazonDbContext context) : base(context)
        {
        }



        public Cart CreateOrUpdate(Guid? cartId, Guid productId, int quantity = 1)
        {
            // معرفة هل الكرت جديد ام لا
            (var cart, var isNewCart)  = GetCart(cartId);

            // إضافة منتج على الكرت
            AddProductToCart(cart, productId, quantity);

            if (isNewCart)
                context.Add(cart);

            else
                context.Update(cart);

            return cart;
        }




        private (Cart cart, bool isNewCart) GetCart(Guid? cartId)
        {
            Cart? cart = null;
            bool isNewCart = false;

            if(cartId is not null || cartId == Guid.Empty)
                cart = context.Carts
                    .Include(c => c.OrderItems)
                    .FirstOrDefault(c => c.CartId == cartId);

            if (cart is null)
            {
                cart = new Cart();
                isNewCart = true;
            }

            return (cart, isNewCart);
        }



        private void AddProductToCart(Cart cart, Guid productId, int quantity)
        {
            var orderItem = cart.OrderItems.FirstOrDefault(oi => oi.ProductId == productId);

            if (orderItem != null && quantity == 0) 
                cart.OrderItems.Remove(orderItem);


            else if(orderItem != null)
                orderItem.Quantity = quantity;


            else
            {
                orderItem = new OrderItem() {ProductId = productId,Quantity = quantity };

                context.OrderItems.Add(orderItem);

                cart.OrderItems.Add(orderItem);
            }
        }

        public Cart GetCartWithDetails(Guid? cartId)
        {
            var cart = context.Carts
                .Include(c => c.OrderItems).ThenInclude(oi => oi.Product)
                .FirstOrDefault(c => c.CartId == cartId);

            return cart;
        }
    }
}
