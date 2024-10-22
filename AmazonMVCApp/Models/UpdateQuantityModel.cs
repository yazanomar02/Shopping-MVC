namespace AmazonMVCApp.Models
{
    public class UpdateQuantityModel
    {
        public Guid CartId { get; set; }
        public List<ProductModel>? Products { get; set; }
    }
}
