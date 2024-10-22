using Amazon.Infrastructure.Repositorys;
using AmazonMVCApp.Models;
using AmazonMVCApp.Repo;
using Microsoft.AspNetCore.Mvc;

namespace AmazonMVCApp.Components
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IStateRepository stateRepository;
        private readonly ICartRepository cartRepository;

        public ShoppingCartViewComponent(IStateRepository stateRepository, ICartRepository cartRepository)
        {
            this.stateRepository = stateRepository;
            this.cartRepository = cartRepository;
        }



        public async Task<IViewComponentResult> InvokeAsync(string cartId)
        {
            if (!Guid.TryParse(cartId, out var id)) // إذا نحج التحويل ستخزن القيمة في id | إذا لم ينجح التحويل سينتقل لل return 
                return View(new ShoppingCartModel());


            var cart = cartRepository.GetCartWithDetails(id);

            if (cart != null)
            {
                stateRepository.SetValue("NumberOfItems", cart.OrderItems.Sum(oi => oi.Quantity).ToString());
                stateRepository.SetValue("CartId", cart.CartId.ToString());
            }


            return View(new ShoppingCartModel() { Cart = cart });
        }
    }
}
