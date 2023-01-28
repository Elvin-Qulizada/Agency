using System.ComponentModel.DataAnnotations;

namespace Agency.ViewModels
{
    public class LoginViewModel
    {
        [MaxLength(20)]
        public string Username { get; set; }
        [MaxLength(20),MinLength(8),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
