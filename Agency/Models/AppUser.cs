using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Agency.Models
{
    public class AppUser : IdentityUser
    {
        [MaxLength(40)]
        public string Fullname { get; set; }
        [MaxLength(40),MinLength(8),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
