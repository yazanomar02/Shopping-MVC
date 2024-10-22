using System.ComponentModel.DataAnnotations;

namespace AmazonMVCApp.Models
{
    public class LoginModel
    {
        [EmailAddress]
        public required string UserName { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
