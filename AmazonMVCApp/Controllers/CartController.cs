using Amazon.Domain.Models;
using Amazon.Infrastructure.Repositorys;
using AmazonMVCApp.Models;
using AmazonMVCApp.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonMVCApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ILogger<CartController> logger;
        private readonly ICartRepository cartRepository;
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IStateRepository stateRepository;

        public CartController(
            ILogger<CartController> logger,
            ICartRepository cartRepository,
            IRepository<Customer> customerRepository,
            IRepository<Order> orderRepository,
            IStateRepository stateRepository
            )
        {
            this.logger = logger;
            this.cartRepository = cartRepository;
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
            this.stateRepository = stateRepository;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult AddToCart(AddToCart addToCartModel)
        {
            if (addToCartModel == null || addToCartModel.Product == null)
                return BadRequest("missing cart/product information");

            var cart = cartRepository.CreateOrUpdate
                (addToCartModel.CartId,
                addToCartModel.Product.ProductId,
                addToCartModel.Product.Quantity
                );

            cartRepository.SaveChanges();

            //Add to session
            stateRepository.SetValue("CartId", cart.CartId.ToString());
            stateRepository.SetValue("NumberOfItems", cart.OrderItems.Sum(oi => oi.Quantity).ToString());


            return RedirectToAction("Index");

        }



        [HttpPost]
        [Route("update")]
        public IActionResult UpdateOrder(UpdateQuantityModel updateQuantityModel)
        {
            if (updateQuantityModel == null)
            {
                return BadRequest("missing model");
            }

            Cart cart = null;

            foreach (var product in updateQuantityModel.Products)
            {
                cart = cartRepository.CreateOrUpdate(
                    updateQuantityModel.CartId,
                    product.ProductId,
                    product.Quantity);
            }

            cartRepository.SaveChanges();

            stateRepository.SetValue("CartId", cart.CartId.ToString());
            stateRepository.SetValue("NumberOfItems", cart.OrderItems.Sum(oi => oi.Quantity).ToString());

            return RedirectToAction("Index", "Cart");
        }




        [HttpPost]
        [ValidateAntiForgeryToken] // للحماية من أتاك CSRF ( CSRF Token )
        [Route("Order")]
        public IActionResult CreateOrder(CreateOrderModel createOrderModel)
        {
            if (createOrderModel.Customer is null)
                ModelState.AddModelError("Customer", "You have to provide a valid customer information");


            if (createOrderModel?.Customer?.Name.Length <= 2)
                ModelState.AddModelError("Customer", "Name length should be more than 2 chars");


            if (!ModelState.IsValid)
                return View("Index");


            var customer = new Customer
            {
                Email = createOrderModel.Customer.Email,
                Name = createOrderModel.Customer.Name,
                City = createOrderModel.Customer.City,
                Country = createOrderModel.Customer.Country,
                PostalCode = createOrderModel.Customer.PostalCode,
                ShippingAddress = createOrderModel.Customer.ShippingAddress,
            };

            customerRepository.Add(customer);


            var order = new Order {CustomerId = customer.CustomerId }; 


            if(createOrderModel.CartId == null || createOrderModel.CartId == Guid.Empty)
            {
                ModelState.AddModelError("Cart", "Missing or deleted shopping cart");
                return View("Index");
            }

            var cart = cartRepository.Get(createOrderModel.CartId.Value);

            if (cart == null)
            {
                ModelState.AddModelError("Cart", "your shopping has been deleted");
                return View("Index");
            }

            foreach(var item  in cart.OrderItems)
            {
                order.OrderItems.Add(item);
            }

            orderRepository.Add(order);

            cartRepository.Update(cart);

            cartRepository.SaveChanges();

            stateRepository.Remove("CartId");
            stateRepository.Remove("NumberOfItems");

            return RedirectToAction("ThankYou");
        }
    }
}

