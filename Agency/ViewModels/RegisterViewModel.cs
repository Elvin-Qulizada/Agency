using System.ComponentModel.DataAnnotations;

namespace Agency.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(40)]
        public string Fullname { get; set; }
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [MaxLength(20), MinLength(8), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(20), MinLength(8), DataType(DataType.Password), Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MaxLength(30), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
