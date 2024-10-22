using Amazon.Domain.Models;
using Amazon.Infrastructure.Repositorys;
using AmazonMVCApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AmazonMVCApp.Components
{
    public class ProductListViewComponent : ViewComponent
    {
        private readonly ILogger<ProductListViewComponent> logger;
        private readonly IRepository<Product> productRepository;

        public ProductListViewComponent(
            ILogger<ProductListViewComponent> logger,
            IRepository<Product> productRepository
            )
        {
            this.logger = logger;
            this.productRepository = productRepository;
        }



        public Task<IViewComponentResult> InvokeAsync()
        {
            var products = productRepository.GetAll();

            return Task.FromResult<IViewComponentResult>(View (products));
        }
    }
}
