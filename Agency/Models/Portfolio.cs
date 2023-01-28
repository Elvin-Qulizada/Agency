using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agency.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(60)]
        public string? ImageUrl{ get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public Category? Category { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}
