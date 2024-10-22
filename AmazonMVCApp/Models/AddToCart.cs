namespace AmazonMVCApp.Models
{
    public class AddToCart
    {
        public Guid CartId { get; set; }
        public ProductModel Product { get; set; }
    }
}
