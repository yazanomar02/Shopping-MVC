using Amazon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.DbContexts
{
    public class AmazonDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var onConfiguringString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = AmazonMVC_DB";

            optionsBuilder.UseSqlServer(onConfiguringString);

            base.OnConfiguring(optionsBuilder);
        }


        public static void CreateInitialTestingDatabase(AmazonDbContext context)
        {
            // Delete if exists
            context.Database.EnsureDeleted();

            // Migration (Create and apply migrations)
            context.Database.Migrate();

            // Add dummy data (Seeding data)
            context.Products.Add(new Product
            {
                Name = "IPhone 16",
                Price = 100,
                ProductId = Guid.Parse("2c6d30b7-9115-426b-a37c-ce27c74ba428")
            });

            context.Products.Add(new Product
            {
                Name = "Samsung S25+",
                Price = 120,
                ProductId = Guid.Parse("d30342fb-0c0f-4cd7-ab15-8032aaec197d")
            });

            context.Products.Add(new Product
            {
                Name = "Xiaomi 11",
                Price = 75,
                ProductId = Guid.Parse("a1001f68-5f86-4332-8665-319b01141fdc")
            });

            context.SaveChanges();
        }
    }
}
