using System.ComponentModel.DataAnnotations;

namespace Agency.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
